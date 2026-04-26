using System;
using System.Threading.Tasks;

namespace Qwen359b.Services;

public interface IVideoService
{
    /// <summary>
    /// Plays the video file asynchronously.
    /// </summary>
    /// <param name="filePath">The absolute path to the video file.</param>
    Task PlayVideoAsync(string filePath);

    /// <summary>
    /// Stops any currently playing video stream.
    /// </summary>
    void StopVideoAsync();

    /// <summary>
    /// Checks if a video is currently playing.
    /// </summary>
    bool IsPlaying { get; }
}