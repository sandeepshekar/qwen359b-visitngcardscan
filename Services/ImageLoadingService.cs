using System;
using System.IO;
using System.Threading.Tasks;
using Qwen359b.Services;

namespace Qwen359b.Services;

/// <summary>
/// Concrete implementation of IImageLoadingService using MAUI/platform capabilities for caching.
/// </summary>
public class ImageLoadingService : IImageLoadingService
{
    // In a real application, this would use platform-specific image libraries (e.g., SkiaSharp, FFImageLoading).

    private readonly string _cacheDirectory;

    public ImageLoadingService()
    {
        // Initialize cache directory path
        string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        _cacheDirectory = Path.Combine(appData, "ImageCache");
        if (!Directory.Exists(_cacheDirectory))
        {
            Directory.CreateDirectory(_cacheDirectory);
        }
    }

    public async Task<Stream> LoadImageAsync(string filePath)
    {
        // 1. Check Cache (Simulated): In a real app, check local cache first.
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var extension = Path.GetExtension(filePath).ToLower();
        var cachedPath = Path.Combine(_cacheDirectory, $"{fileName}_{extension}.jpg"); // Using .jpg as a generic cache extension

        if (File.Exists(cachedPath))
        {
            Console.WriteLine($"[ImageLoader] Cache hit for: {filePath}. Loading from cache.");
            await Task.Delay(20); // Faster load time simulation
            // Return a copy of the cached file to ensure it's not disposed by the caller
            return new MemoryStream(File.ReadAllBytes(cachedPath));
        }

        Console.WriteLine($"[ImageLoader] Cache miss for: {filePath}. Loading optimized image from source.");
        await Task.Delay(100); // Simulate I/O delay

        // Load the original file stream
        using (var originalStream = File.OpenRead(filePath))
        {
            // 3. Simulate Caching: Read all bytes and save them to the cache path.
            var imageBytes = new byte[originalStream.Length];
            originalStream.Read(imageBytes, 0, imageBytes.Length);

            // Write to cache directory (simulating optimized processing/resizing)
            File.WriteAllBytes(cachedPath, imageBytes);
            Console.WriteLine($"[ImageLoader] Successfully cached image to: {cachedPath}");

            // Return a new stream from the original bytes for immediate use
            var memoryStream = new MemoryStream(imageBytes);
            return memoryStream;
        }
    }

    public async Task<Stream> GenerateThumbnailAsync(string filePath)
    {
        Console.WriteLine($"[ImageLoader] Generating thumbnail for: {filePath}");
        // Placeholder: Simulate thumbnail generation (e.g., resizing and saving to cache)
        await Task.Delay(100); // Simulate processing delay
        return File.OpenRead(filePath);
    }
}
