using System.Collections.Generic;
using System.Threading.Tasks;
using Qwen359b.Models;

namespace Qwen359b.Services;

public interface IMediaService
{
    /// <summary>
    /// Scans the specified directories recursively to find all media files (images and videos).
    /// </summary>
    /// <param name="directoryPaths">A list of root directories to scan.</param>
    /// <returns>A task that returns a list of discovered MediaItem objects.</returns>
    Task<List<MediaItem>> ScanDirectoriesAsync(IEnumerable<string> directoryPaths);
}