using System;
using System.Threading.Tasks;

namespace Qwen359b.Services;

/// <summary>
/// Defines a cross-platform abstraction layer for native media player controls.
/// </summary>
public interface IPlatformMediaPlayer : IDisposable
{
    /// <summary>
    /// Initializes the player with a specific file path and prepares it for playback.
    /// </summary>
    /// <param name="filePath">The absolute path to the media file.</param>
    Task InitializeAsync(string filePath);

    /// <summary>
    /// Starts playing the initialized media stream.
    /// </summary>
    void Play();

    /// <summary>
    /// Pauses the currently playing media stream.
    /// </summary>
    void Pause();

    /// <summary>
    /// Stops playback and releases resources.
    /// </summary>
    void Stop();

    /// <summary>
    /// Gets a value indicating whether the player is currently active/playing.
    /// </summary>
    bool IsPlaying { get; }
}