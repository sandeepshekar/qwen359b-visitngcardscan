using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Qwen359b.Models;

namespace Qwen359b.Services;

public class MediaScannerService : IMediaService
{
    private readonly HashSet<string> _imageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
    private readonly HashSet<string> _videoExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { ".mp4", ".mov", ".avi", ".mkv" };

    public async Task<List<MediaItem>> ScanDirectoriesAsync(IEnumerable<string> directoryPaths)
    {
        var allMediaItems = new List<MediaItem>();
        var paths = directoryPaths.ToList();

        foreach (var rootPath in paths)
        {
            if (!Directory.Exists(rootPath))
            {
                Console.WriteLine($"Warning: Directory not found and skipped: {rootPath}");
                continue;
            }

            // Use Directory.EnumerateFiles for efficient, recursive scanning
            try
            {
                var files = Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories);

                foreach (var filePath in files)
                {
                    string extension = Path.GetExtension(filePath);
                    if (_imageExtensions.Contains(extension))
                    {
                        allMediaItems.Add(new MediaItem
                        {
                            FilePath = filePath,
                            FileName = Path.GetFileName(filePath),
                            MediaType = MediaType.Image,
                            ThumbnailSource = $"file://{filePath}" // Placeholder for thumbnail source
                        });
                    }
                    else if (_videoExtensions.Contains(extension))
                    {
                        allMediaItems.Add(new MediaItem
                        {
                            FilePath = filePath,
                            FileName = Path.GetFileName(filePath),
                            MediaType = MediaType.Video,
                            ThumbnailSource = null // Videos usually don't have simple thumbnails
                        });
                    }
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"Access denied to directory {rootPath}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while scanning {rootPath}: {ex.Message}");
            }
        }

        return allMediaItems;
    }
}