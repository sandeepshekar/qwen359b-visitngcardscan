using Microsoft.Maui.Controls;
using Qwen359b.Models;
using Qwen359b.Services;

namespace Qwen359b.Controls;

public partial class CustomVideoPlayerControl : ContentView
{
    private readonly IPlatformMediaPlayer _player;
    private readonly IImageLoaderService _imageLoaderService;

    public CustomVideoPlayerControl(IPlatformMediaPlayer player, IImageLoaderService imageLoaderService)
    {
        InitializeComponent();
        _player = player ?? throw new ArgumentNullException(nameof(player));
        _imageLoaderService = imageLoaderService ?? throw new ArgumentNullException(nameof(imageLoaderService));
    }

    public async Task InitializeAndPlayAsync(string filePath, MediaItem mediaItem)
    {
        if (_player == null)
        {
            // Fallback or error handling if dependency injection failed
            await Shell.Current.DisplayAlert("Error", "Video player service is not initialized.", "OK");
            return;
        }

        try
        {
            await _player.InitializeAsync(filePath);
            await Task.Delay(100); // Give time for native resources to initialize
            _player.Play();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to play video: {ex.Message}");
        }
    }

    public void StopPlayback()
    {
        if (_player != null)
        {
            _player.Stop();
        }
    }
}