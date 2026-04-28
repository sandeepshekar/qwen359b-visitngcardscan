using System;
using System.IO;
using System.Threading.Tasks;

namespace Qwen359b.Services;

/// <summary>
/// Interface for optimized image loading, handling caching and asynchronous retrieval.
/// </summary>
public interface IImageLoaderService
{
    /// <summary>
    /// Loads an image from a file path asynchronously, utilizing cache if available.
    /// </summary>
    /// <param name="filePath">The absolute path to the image file.</param>
    /// <returns>A stream or source suitable for MAUI Image control.</returns>
    Task<Stream> LoadImageAsync(string filePath);

    /// <summary>
    /// Generates a thumbnail from a full-size image file.
    /// </summary>
    /// <param name="filePath">The absolute path to the original image file.</param>
    /// <returns>A stream representing the generated thumbnail.</returns>
    Task<Stream> GenerateThumbnailAsync(string filePath);
}
