namespace MediaDeckSite.Services;

/// <summary>
/// リバースプロキシ配下のリクエストからクライアント IP アドレスを抽出するヘルパー。
/// </summary>
public static class ClientIpResolver
{
    private const int MaxIpLength = 64;

    /// <summary>
    /// CF-Connecting-IP、X-Real-IP、RemoteIpAddress の優先順でクライアント IP を取得する。
    /// </summary>
    public static string GetClientIp(HttpContext context)
    {
        var candidate =
            ExtractFirstHeaderValue(context.Request.Headers["CF-Connecting-IP"])
            ?? ExtractFirstHeaderValue(context.Request.Headers["X-Real-IP"])
            ?? context.Connection.RemoteIpAddress?.ToString();

        if (string.IsNullOrWhiteSpace(candidate))
        {
            return "unknown";
        }

        return candidate.Length <= MaxIpLength ? candidate : candidate[..MaxIpLength];
    }

    private static string? ExtractFirstHeaderValue(string? headerValue)
    {
        if (string.IsNullOrWhiteSpace(headerValue))
        {
            return null;
        }

        var commaIndex = headerValue.IndexOf(',');
        var first = commaIndex >= 0 ? headerValue[..commaIndex] : headerValue;
        var trimmed = first.Trim();
        return string.IsNullOrEmpty(trimmed) ? null : trimmed;
    }
}
