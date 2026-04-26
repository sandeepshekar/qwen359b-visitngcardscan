using Microsoft.Maui.Controls;
using Qwen359b.Services;
using System;
using System.Threading.Tasks;

namespace Qwen359b.Controls;

public partial class CustomVideoPlayerControl : ContentView
{
    private readonly IPlatformMediaPlayer _player;
    private readonly IImageLoadingService _imageLoaderService; // Keep this for potential image fallback/thumbnail display

    public CustomVideoPlayerControl()
    {
        InitializeComponent();
        // Dependency Injection will provide the correct platform handler implementation.
        _player = (IPlatformMediaPlayer)DependencyService.Get("PlatformMediaPlayer"); 
        _imageLoaderService = DependencyService.Get<IImageLoadingService>();
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