using MediaDeckSite.Data;

namespace MediaDeckSite.Services;

/// <summary>
/// 受信したフィードバックを Slack へ通知するサービスのインターフェース。
/// </summary>
public interface ISlackNotifier
{
    Task NotifyFeedbackAsync(FeedbackEntity feedback, CancellationToken cancellationToken = default);
}
