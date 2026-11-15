using MediaDeckSite.Data;
using MediaDeckSite.Models;
using MediaDeckSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace MediaDeckSite.Controllers;

/// <summary>
/// MediaDeck クライアントからのフィードバックを受信して PostgreSQL へ保存する API コントローラ。
/// </summary>
[ApiController]
[Route("api/feedback")]
public class FeedbackController(AppDbContext dbContext, ILogger<FeedbackController> logger) : ControllerBase
{
    private const int MaxTitleLength = 500;
    private const int MaxDescriptionLength = 50_000;
    private const int MaxContactLength = 500;
    private const int MaxScreenshotBytes = 10 * 1024 * 1024;
    private const int MaxScreenshotBase64Length = ((MaxScreenshotBytes + 2) / 3) * 4 + 4;

    private static readonly HashSet<string> AllowedCategories = new(StringComparer.Ordinal)
    {
        "BugReport",
        "FeatureRequest",
        "Other",
    };

    /// <summary>
    /// フィードバックを受信・検証し、データベースへ保存する。
    /// </summary>
    [HttpPost]
    [EnableRateLimiting("feedback")]
    [RequestSizeLimit(15 * 1024 * 1024)]
    public async Task<IActionResult> PostAsync([FromBody] FeedbackRequest? request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return BadRequest(new { error = "Request body is required." });
        }

        if (string.IsNullOrWhiteSpace(request.Category) || !AllowedCategories.Contains(request.Category))
        {
            return BadRequest(new { error = "Invalid category. Allowed values are BugReport, FeatureRequest, Other." });
        }

        var title = request.Title?.Trim();
        if (string.IsNullOrEmpty(title))
        {
            return BadRequest(new { error = "Title is required." });
        }

        if (title.Length > MaxTitleLength)
        {
            return BadRequest(new { error = $"Title must not exceed {MaxTitleLength} characters." });
        }

        var description = request.Description?.Trim();
        if (string.IsNullOrEmpty(description))
        {
            return BadRequest(new { error = "Description is required." });
        }

        if (description.Length > MaxDescriptionLength)
        {
            return BadRequest(new { error = $"Description must not exceed {MaxDescriptionLength} characters." });
        }

        if (request.SubmittedAt is null)
        {
            return BadRequest(new { error = "SubmittedAt is required." });
        }

        var contact = string.IsNullOrWhiteSpace(request.Contact) ? null : request.Contact.Trim();
        if (contact is not null && contact.Length > MaxContactLength)
        {
            return BadRequest(new { error = $"Contact must not exceed {MaxContactLength} characters." });
        }

        var screenshotResult = TryDecodeScreenshot(request.ScreenshotBase64);
        if (screenshotResult.ErrorResult is not null)
        {
            return screenshotResult.ErrorResult;
        }

        var entity = new FeedbackEntity
        {
            Category = request.Category,
            Title = title,
            Description = description,
            Contact = contact,
            EnvironmentInfo = string.IsNullOrWhiteSpace(request.EnvironmentInfo) ? null : request.EnvironmentInfo,
            LogContent = string.IsNullOrWhiteSpace(request.LogContent) ? null : request.LogContent,
            ScreenshotData = screenshotResult.Data,
            ScreenshotMimeType = screenshotResult.MimeType,
            ScreenshotExtension = screenshotResult.Extension,
            SubmittedAt = request.SubmittedAt.Value.ToUniversalTime(),
            ReceivedAt = DateTimeOffset.UtcNow,
            ClientIp = ClientIpResolver.GetClientIp(HttpContext),
        };

        dbContext.Feedbacks.Add(entity);
        await dbContext.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Feedback {FeedbackId} saved (Category: {Category})", entity.Id, entity.Category);

        return Ok(new { ok = true });
    }

    private (byte[]? Data, string? MimeType, string? Extension, IActionResult? ErrorResult) TryDecodeScreenshot(string? base64)
    {
        if (string.IsNullOrWhiteSpace(base64))
        {
            return (null, null, null, null);
        }

        var trimmed = base64.Trim();
        if (trimmed.Length > MaxScreenshotBase64Length)
        {
            return (null, null, null, StatusCode(StatusCodes.Status413PayloadTooLarge, new { error = "Screenshot exceeds 10 MB limit." }));
        }

        byte[] bytes;
        try
        {
            bytes = Convert.FromBase64String(trimmed);
        }
        catch (FormatException)
        {
            return (null, null, null, BadRequest(new { error = "ScreenshotBase64 is not valid Base64." }));
        }

        if (bytes.Length == 0)
        {
            return (null, null, null, BadRequest(new { error = "Screenshot image data is empty." }));
        }

        if (bytes.Length > MaxScreenshotBytes)
        {
            return (null, null, null, StatusCode(StatusCodes.Status413PayloadTooLarge, new { error = "Screenshot exceeds 10 MB limit." }));
        }

        if (!ImageInspector.TryDetectFormat(bytes, out var mimeType, out var extension))
        {
            return (null, null, null, BadRequest(new { error = "Unsupported screenshot image format." }));
        }

        return (bytes, mimeType, extension, null);
    }
}
