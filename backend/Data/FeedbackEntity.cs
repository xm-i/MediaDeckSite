namespace MediaDeckSite.Data;

/// <summary>
/// PostgreSQL の feedbacks テーブルに対応するフィードバックエンティティ。
/// </summary>
public class FeedbackEntity
{
    public long Id { get; set; }
    public string Category { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Contact { get; set; }
    public string? EnvironmentInfo { get; set; }
    public string? LogContent { get; set; }
    public byte[]? ScreenshotData { get; set; }
    public string? ScreenshotMimeType { get; set; }
    public string? ScreenshotExtension { get; set; }
    public DateTimeOffset SubmittedAt { get; set; }
    public DateTimeOffset ReceivedAt { get; set; }
    public string? ClientIp { get; set; }
}
