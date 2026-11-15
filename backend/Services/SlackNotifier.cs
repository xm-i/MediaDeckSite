using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using MediaDeckSite.Data;
using MediaDeckSite.Options;
using Microsoft.Extensions.Options;

namespace MediaDeckSite.Services;

/// <summary>
/// Slack Bot API (chat.postMessage + Files V2) を用いてフィードバックを通知するサービス。
/// </summary>
public class SlackNotifier(
    HttpClient httpClient,
    IOptions<SlackOptions> options,
    ILogger<SlackNotifier> logger) : ISlackNotifier
{
    private const string PostMessageEndpoint = "https://slack.com/api/chat.postMessage";
    private const string GetUploadUrlEndpoint = "https://slack.com/api/files.getUploadURLExternal";
    private const string CompleteUploadEndpoint = "https://slack.com/api/files.completeUploadExternal";
    private const int MaxMrkdwnDescriptionLength = 2500;
    private const int MaxMrkdwnEnvironmentLength = 1500;
    private static readonly TimeSpan JstOffset = TimeSpan.FromHours(9);

    public async Task NotifyFeedbackAsync(FeedbackEntity feedback, CancellationToken cancellationToken = default)
    {
        var slack = options.Value;
        if (!IsConfigured(slack))
        {
            logger.LogDebug("Skipping Slack notification because Slack BotToken or ChannelId is not configured.");
            return;
        }

        var parentTs = await PostSummaryMessageAsync(slack, feedback, cancellationToken);
        var attachments = BuildAttachments(feedback);
        if (attachments.Count == 0)
        {
            return;
        }

        var uploadedFiles = new List<UploadedSlackFile>(attachments.Count);
        foreach (var attachment in attachments)
        {
            var fileId = await UploadSingleFileAsync(slack.BotToken, attachment, cancellationToken);
            uploadedFiles.Add(new UploadedSlackFile(fileId, attachment.Title));
        }

        await CompleteUploadAsync(slack, parentTs, uploadedFiles, cancellationToken);
    }

    private static bool IsConfigured(SlackOptions slack) =>
        !string.IsNullOrWhiteSpace(slack.BotToken)
        && !string.IsNullOrWhiteSpace(slack.ChannelId)
        && !slack.BotToken.Contains("CHANGE_ME", StringComparison.Ordinal)
        && !slack.ChannelId.Contains("CHANGE_ME", StringComparison.Ordinal);

    private async Task<string> PostSummaryMessageAsync(
        SlackOptions slack,
        FeedbackEntity feedback,
        CancellationToken cancellationToken)
    {
        var categoryLabel = feedback.Category switch
        {
            "BugReport" => ":beetle: バグ報告 (BugReport)",
            "FeatureRequest" => ":sparkles: 機能要望 (FeatureRequest)",
            _ => ":speech_balloon: その他 (Other)",
        };

        var headerText = $"[{FormatShortCategory(feedback.Category)}] #{feedback.Id} {feedback.Title}";
        if (headerText.Length > 150)
        {
            headerText = string.Concat(headerText.AsSpan(0, 147), "...");
        }

        var description = feedback.Description;
        var isDescriptionTruncated = description.Length > MaxMrkdwnDescriptionLength;
        var displayedDescription = isDescriptionTruncated
            ? $"{description[..MaxMrkdwnDescriptionLength]}\n...（全文はスレッドの description.txt を参照）"
            : description;

        var submittedJst = feedback.SubmittedAt.ToOffset(JstOffset).ToString("yyyy-MM-dd HH:mm:ss 'JST'", CultureInfo.InvariantCulture);
        var contactText = string.IsNullOrWhiteSpace(feedback.Contact) ? "未入力" : EscapeMrkdwn(feedback.Contact);

        var blocks = new List<object>
        {
            new
            {
                type = "header",
                text = new
                {
                    type = "plain_text",
                    text = headerText,
                    emoji = true,
                },
            },
            new
            {
                type = "section",
                fields = new object[]
                {
                    new { type = "mrkdwn", text = $"*種別:*\n{categoryLabel}" },
                    new { type = "mrkdwn", text = $"*送信日時:*\n{submittedJst}" },
                    new { type = "mrkdwn", text = $"*連絡先:*\n{contactText}" },
                    new { type = "mrkdwn", text = $"*DB ID:*\n`#{feedback.Id}`" },
                },
            },
            new
            {
                type = "section",
                text = new
                {
                    type = "mrkdwn",
                    text = $"*詳細内容:*\n{EscapeMrkdwn(displayedDescription)}",
                },
            },
        };

        if (!string.IsNullOrWhiteSpace(feedback.EnvironmentInfo))
        {
            var env = feedback.EnvironmentInfo.Trim().Replace("```", "'''", StringComparison.Ordinal);
            if (env.Length > MaxMrkdwnEnvironmentLength)
            {
                env = $"{env[..MaxMrkdwnEnvironmentLength]}\n...";
            }

            blocks.Add(new
            {
                type = "section",
                text = new
                {
                    type = "mrkdwn",
                    text = $"*実行環境:*\n```{EscapeMrkdwn(env)}```",
                },
            });
        }

        var payload = new
        {
            channel = slack.ChannelId,
            text = $"{categoryLabel} #{feedback.Id}: {feedback.Title}",
            blocks,
        };

        using var doc = await PostJsonToSlackApiAsync(PostMessageEndpoint, slack.BotToken, payload, cancellationToken);
        if (!doc.RootElement.TryGetProperty("ts", out var tsProp) || string.IsNullOrWhiteSpace(tsProp.GetString()))
        {
            throw new InvalidOperationException("Slack chat.postMessage response did not include 'ts'.");
        }

        return tsProp.GetString()!;
    }

    private static List<SlackAttachment> BuildAttachments(FeedbackEntity feedback)
    {
        var list = new List<SlackAttachment>();

        if (feedback.ScreenshotData is { Length: > 0 })
        {
            var ext = string.IsNullOrWhiteSpace(feedback.ScreenshotExtension) ? ".bin" : feedback.ScreenshotExtension;
            list.Add(new SlackAttachment(
                $"feedback-{feedback.Id}-screenshot{ext}",
                "スクリーンショット",
                feedback.ScreenshotData));
        }

        if (!string.IsNullOrWhiteSpace(feedback.LogContent))
        {
            list.Add(new SlackAttachment(
                $"feedback-{feedback.Id}-app.log",
                "アプリログ",
                Encoding.UTF8.GetBytes(feedback.LogContent)));
        }

        if (feedback.Description.Length > MaxMrkdwnDescriptionLength)
        {
            list.Add(new SlackAttachment(
                $"feedback-{feedback.Id}-description.txt",
                "詳細全文",
                Encoding.UTF8.GetBytes(feedback.Description)));
        }

        return list;
    }

    private async Task<string> UploadSingleFileAsync(
        string botToken,
        SlackAttachment attachment,
        CancellationToken cancellationToken)
    {
        using var getUrlReq = new HttpRequestMessage(HttpMethod.Post, GetUploadUrlEndpoint);
        getUrlReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", botToken);
        getUrlReq.Content = new FormUrlEncodedContent(
        [
            new KeyValuePair<string, string>("filename", attachment.FileName),
            new KeyValuePair<string, string>("length", attachment.Content.Length.ToString(CultureInfo.InvariantCulture)),
        ]);

        using var getUrlRes = await httpClient.SendAsync(getUrlReq, cancellationToken);
        using var getUrlDoc = await ParseSlackApiResponseAsync(getUrlRes, "files.getUploadURLExternal", cancellationToken);

        var uploadUrl = getUrlDoc.RootElement.GetProperty("upload_url").GetString()
            ?? throw new InvalidOperationException("Slack files.getUploadURLExternal did not return upload_url.");
        var fileId = getUrlDoc.RootElement.GetProperty("file_id").GetString()
            ?? throw new InvalidOperationException("Slack files.getUploadURLExternal did not return file_id.");

        using var uploadReq = new HttpRequestMessage(HttpMethod.Post, uploadUrl);
        uploadReq.Content = new ByteArrayContent(attachment.Content);
        uploadReq.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

        using var uploadRes = await httpClient.SendAsync(uploadReq, cancellationToken);
        if (!uploadRes.IsSuccessStatusCode)
        {
            var errorBody = await uploadRes.Content.ReadAsStringAsync(cancellationToken);
            throw new InvalidOperationException($"Slack external binary upload failed ({(int)uploadRes.StatusCode}): {errorBody}");
        }

        return fileId;
    }

    private async Task CompleteUploadAsync(
        SlackOptions slack,
        string threadTs,
        IReadOnlyList<UploadedSlackFile> files,
        CancellationToken cancellationToken)
    {
        var payload = new
        {
            files = files.Select(f => new { id = f.FileId, title = f.Title }).ToArray(),
            channel_id = slack.ChannelId,
            thread_ts = threadTs,
        };

        using var _ = await PostJsonToSlackApiAsync(CompleteUploadEndpoint, slack.BotToken, payload, cancellationToken);
    }

    private async Task<JsonDocument> PostJsonToSlackApiAsync(
        string endpoint,
        string botToken,
        object payload,
        CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(payload);
        using var req = new HttpRequestMessage(HttpMethod.Post, endpoint);
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", botToken);
        req.Content = new StringContent(json, Encoding.UTF8, "application/json");

        using var res = await httpClient.SendAsync(req, cancellationToken);
        return await ParseSlackApiResponseAsync(res, endpoint, cancellationToken);
    }

    private static async Task<JsonDocument> ParseSlackApiResponseAsync(
        HttpResponseMessage response,
        string apiName,
        CancellationToken cancellationToken)
    {
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"Slack API {apiName} returned HTTP {(int)response.StatusCode}: {body}");
        }

        var doc = JsonDocument.Parse(body);
        if (!doc.RootElement.TryGetProperty("ok", out var okProp) || !okProp.GetBoolean())
        {
            var error = doc.RootElement.TryGetProperty("error", out var errProp) ? errProp.GetString() : "unknown_error";
            doc.Dispose();
            throw new InvalidOperationException($"Slack API {apiName} returned ok=false: {error}");
        }

        return doc;
    }

    private static string FormatShortCategory(string category) => category switch
    {
        "BugReport" => "バグ報告",
        "FeatureRequest" => "機能要望",
        _ => "その他",
    };

    private static string EscapeMrkdwn(string text) =>
        text.Replace("&", "&amp;", StringComparison.Ordinal)
            .Replace("<", "&lt;", StringComparison.Ordinal)
            .Replace(">", "&gt;", StringComparison.Ordinal);

    private sealed record SlackAttachment(string FileName, string Title, byte[] Content);
    private sealed record UploadedSlackFile(string FileId, string Title);
}
