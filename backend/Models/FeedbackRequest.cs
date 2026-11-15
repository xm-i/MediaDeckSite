namespace MediaDeckSite.Models;

/// <summary>
/// クライアントからのフィードバック送信リクエスト DTO。
/// </summary>
public class FeedbackRequest
{
    public string? Category { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? SubmittedAt { get; set; }
    public string? Contact { get; set; }
    public string? EnvironmentInfo { get; set; }
    public string? LogContent { get; set; }
    public string? ScreenshotBase64 { get; set; }
}
