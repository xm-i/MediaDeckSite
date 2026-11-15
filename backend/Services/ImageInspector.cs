namespace MediaDeckSite.Services;

/// <summary>
/// 画像バイナリのマジックバイトから MIME タイプおよび拡張子を判定するヘルパー。
/// </summary>
public static class ImageInspector
{
    private static readonly byte[] PngMagic = [0x89, 0x50, 0x4E, 0x47];
    private static readonly byte[] JpegMagic = [0xFF, 0xD8, 0xFF];
    private static readonly byte[] GifMagic = [0x47, 0x49, 0x46, 0x38];
    private static readonly byte[] RiffMagic = [0x52, 0x49, 0x46, 0x46];
    private static readonly byte[] WebpMagic = [0x57, 0x45, 0x42, 0x50];
    private static readonly byte[] BmpMagic = [0x42, 0x4D];

    /// <summary>
    /// 画像データの先頭バイト列からサポート対象形式（PNG/JPEG/GIF/WEBP/BMP）を判定する。
    /// </summary>
    public static bool TryDetectFormat(ReadOnlySpan<byte> data, out string mimeType, out string extension)
    {
        if (data.StartsWith(PngMagic))
        {
            mimeType = "image/png";
            extension = ".png";
            return true;
        }

        if (data.StartsWith(JpegMagic))
        {
            mimeType = "image/jpeg";
            extension = ".jpg";
            return true;
        }

        if (data.StartsWith(GifMagic))
        {
            mimeType = "image/gif";
            extension = ".gif";
            return true;
        }

        if (data.Length >= 12 && data[..4].SequenceEqual(RiffMagic) && data[8..12].SequenceEqual(WebpMagic))
        {
            mimeType = "image/webp";
            extension = ".webp";
            return true;
        }

        if (data.StartsWith(BmpMagic))
        {
            mimeType = "image/bmp";
            extension = ".bmp";
            return true;
        }

        mimeType = string.Empty;
        extension = string.Empty;
        return false;
    }
}
