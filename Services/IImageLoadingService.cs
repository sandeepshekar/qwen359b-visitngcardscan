using System;
using System.IO;
using System.Threading.Tasks;

namespace Qwen359b.Services;

/// <summary>
/// Interface for optimized image loading, handling caching and asynchronous retrieval.
/// </summary>
public interface IImageLoadingService
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
    Task<Stream> GenerateThumbnailAsync(string filePath, int width = 128, int height = 128);

    /// <summary>
    /// Validates whether an image file is valid and accessible.
    /// </summary>
    Task<bool> ValidateImageAsync(string filePath);

    /// <summary>
    /// Clears the image cache.
    /// </summary>
    void ClearCache();

    /// <summary>
    /// Gets the dimensions of an image as a string.
    /// </summary>
    Task<string> GetImageDimensionsAsync(string filePath);
}