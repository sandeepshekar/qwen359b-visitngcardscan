using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qwen359b.Services;

public interface IFileSelectionService
{
    /// <summary>
    /// Asynchronously allows the user to select one or more local directories containing media files.
    /// </summary>
    /// <returns>A list of selected directory paths.</returns>
    Task<List<string>> SelectDirectoriesAsync();
}