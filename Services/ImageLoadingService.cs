using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Qwen359b.Services;

/// <summary>
/// Default implementation of IImageLoadingService for loading and caching images.
/// </summary>
public class ImageLoadingService : IImageLoadingService
{
    private readonly ConcurrentDictionary<string, Stream> _cache = new();
    private const int CacheSize = 10; // Simple cache with limit

    public async Task<Stream> LoadImageAsync(string filePath)
    {
        if (_cache.TryGetValue(filePath, out var cachedStream))
        {
            cachedStream.Seek(0, SeekOrigin.Begin);
            return cachedStream;
        }

        try
        {
            var stream = File.OpenRead(filePath);
            
            // Simple cache management - limit cache size
            if (_cache.Count >= CacheSize)
            {
                var oldestKey = _cache.Keys.First();
                _cache.TryRemove(oldestKey, out var oldStream);
                oldStream?.Dispose();
            }

            _cache[filePath] = stream;
            return stream;
        }
        catch (Exception ex)
        {
            throw new Exception($"Failed to load image from {filePath}", ex);
        }
    }

    public async Task<Stream> GenerateThumbnailAsync(string filePath, int width, int height)
    {
        // Simplified thumbnail generation - for now just return the original image
        // A production implementation would use ImageSharp or similar library
        return await LoadImageAsync(filePath);
    }

    public async Task<bool> ValidateImageAsync(string filePath)
    {
        try
        {
            var fileInfo = new FileInfo(filePath);
            return fileInfo.Exists && fileInfo.Length > 0;
        }
        catch
        {
            return false;
        }
    }

    public void ClearCache()
    {
        foreach (var stream in _cache.Values)
        {
            stream?.Dispose();
        }
        _cache.Clear();
    }

    public async Task<string> GetImageDimensionsAsync(string filePath)
    {
        // Placeholder for getting image dimensions
        // Would need actual image processing library
        return "Unknown";
    }
}
