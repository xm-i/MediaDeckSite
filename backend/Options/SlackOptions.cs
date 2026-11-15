namespace MediaDeckSite.Options;

/// <summary>
/// Slack Bot API を用いた通知設定。
/// </summary>
public class SlackOptions
{
    public const string SectionName = "Slack";

    public string BotToken { get; set; } = string.Empty;
    public string ChannelId { get; set; } = string.Empty;
}
