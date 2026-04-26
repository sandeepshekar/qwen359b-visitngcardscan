namespace Qwen359b.Models;

public enum MediaType
{
    Image,
    Video,
    Unknown
}

public class MediaItem
{
    public string FilePath { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public MediaType MediaType { get; set; } = MediaType.Unknown;
    public string ThumbnailSource { get; set; } = string.Empty; // Source for cached thumbnail images
}