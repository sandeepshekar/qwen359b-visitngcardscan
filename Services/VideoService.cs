using System;
using System.Threading.Tasks;
using Qwen359b.Services.PlatformHandlers;

namespace Qwen359b.Services;

/// <summary>
/// Concrete implementation of IVideoService that uses the platform-specific player handler.
/// </summary>
public class VideoService : IVideoService
{
    private readonly IPlatformMediaPlayer _player;

    // Dependency Injection will provide the correct platform handler (Android, iOS, or Windows)
    public VideoService(IPlatformMediaPlayer player)
    {
        _player = player ?? throw new ArgumentNullException(nameof(player));
    }

    public bool IsPlaying => _player.IsPlaying;

    public async Task PlayVideoAsync(string filePath)
    {
        try
        {
            // 1. Initialize the platform-specific player with the file path
            await _player.InitializeAsync(filePath);
            
            // 2. Start playback
            _player.Play();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error playing video: {ex.Message}");
        }
    }

    public void StopVideoAsync()
    {
        _player.Stop();
    }
}