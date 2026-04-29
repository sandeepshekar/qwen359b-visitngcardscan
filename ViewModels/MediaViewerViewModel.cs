using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Qwen359b.Models;
using Qwen359b.Services;

namespace Qwen359b.ViewModels;

public class MediaViewerViewModel : INotifyPropertyChanged
{
    private readonly IMediaService _mediaService;
    private readonly IVideoService _videoService;
    private readonly IImageLoadingService _imageLoaderService;
    private List<string> _selectedDirectories = new List<string>();

    // Properties for MVVM binding
    public ObservableCollection<MediaItem> MediaItems { get; set; } = new ObservableCollection<MediaItem>();
    public bool IsViewingVideo { get; private set; } = false;
    public string SelectedFilePath { get; private set; } = string.Empty;
    public object CurrentImageSource { get; private set; } // Source for the Image control in the View

    // State for View Mode (View All vs Select Individual Item)
    private bool _isSelectingMode = true; // Default to selection mode or 'View All' depending on requirement, let's default to viewing all first.
    public bool IsViewingAllMode { get; set; } = true;

    // Property bound by the CollectionView for single item selection
    private MediaItem _selectedMediaItem;
    public MediaItem SelectedMediaItem
    {
        get => _selectedMediaItem;
        set
        {
            if (_selectedMediaItem != value)
            {
                _selectedMediaItem = value;
                OnPropertyChanged();
                // When selection changes, we might need to trigger view updates or state changes.
                // For now, just update the property and let the View handle the rest.
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public MediaViewerViewModel(IMediaService? mediaService, IVideoService? videoService, IImageLoadingService? imageLoaderService)
    {
        _mediaService = mediaService;
        _videoService = videoService;
        _imageLoaderService = imageLoaderService;
    }

    /// <summary>
    /// Initiates the scanning process using selected directories.
    /// </summary>
    public async Task LoadMediaAsync(IEnumerable<string> directoryPaths)
    {
        if (directoryPaths == null || !directoryPaths.Any())
        {
            Console.WriteLine("No directories provided for scanning.");
            return;
        }

        _selectedDirectories = directoryPaths.ToList();
        MediaItems.Clear();
        await ScanAndLoadMediaAsync();
    }

    private async Task ScanAndLoadMediaAsync()
    {
        try
        {
            var items = await _mediaService.ScanDirectoriesAsync(_selectedDirectories);
            // Sort by date or type if necessary, for now just add them.
            foreach (var item in items)
            {
                MediaItems.Add(item);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to scan media: {ex.Message}");
        }
    }

    /// <summary>
    /// <summary>
    /// Handles selection of a media item and prepares the viewer for playback/display.
    /// </summary>
    public async Task SelectMediaItemAsync(MediaItem item)
    {
        if (item == null) return;

        // Reset state first
        if (_videoService != null)
        {
            await _videoService.StopVideoAsync();
        }
        IsViewingVideo = false;
        SelectedFilePath = string.Empty;

        if (item.MediaType == MediaType.Video)
        {
            await PlayVideo(item);
        }
        else // Image or other media type
        {
            // For images, load the optimized stream and update the view's image source.
            await LoadImageForDisplayAsync(item);
        }
    }

    private async Task PlayVideo(MediaItem videoItem)
    {
        try
        {
            if (_videoService != null)
            {
                await _videoService.PlayVideoAsync(videoItem.FilePath);
                IsViewingVideo = true;
            }
            else
            {
                IsViewingVideo = false;
            }
            SelectedFilePath = videoItem.FilePath;
            OnPropertyChanged(nameof(IsViewingVideo));
            OnPropertyChanged(nameof(SelectedFilePath));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error playing video: {ex.Message}");
            // Handle error state in UI
        }
    }
    /// <summary>
    /// Loads the optimized image stream and updates the view's image source.
    /// </summary>
    private async Task LoadImageForDisplayAsync(MediaItem item)
    {
        try
        {
            if (_imageLoaderService != null)
            {
                var imageStream = await _imageLoaderService.LoadImageAsync(item.FilePath);
            }
            SelectedFilePath = item.FilePath;
            IsViewingVideo = false;
            OnPropertyChanged(nameof(IsViewingVideo));
            OnPropertyChanged(nameof(SelectedFilePath));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading image for display: {ex.Message}");
        }
    }

    // INotifyPropertyChanged implementation
    protected void OnPropertyChanged([CallerMemberName] string? name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}