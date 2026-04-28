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
    private readonly IImageLoaderService _imageLoaderService;
    private List<string> _selectedDirectories = new List<string>();

    // Properties for MVVM binding
    public ObservableCollection<MediaItem> MediaItems { get; set; } = new ObservableCollection<MediaItem>();
    public bool IsViewingVideo { get; private set; } = false;
    public string SelectedFilePath { get; private set; } = string.Empty;
    public object CurrentImageSource { get; private set; } // Source for the Image control in the View

    // Properties for source generator binding (x:Name attributes)
    public object? MediaDisplayArea => this;
    public object? ImagePresenter => this;
    public object? MediaTitleLabel => this;

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

    public MediaViewerViewModel(IMediaService mediaService, IVideoService videoService, IImageLoaderService imageLoaderService)
    {
        _mediaService = mediaService ?? throw new ArgumentNullException(nameof(mediaService));
        _videoService = videoService ?? throw new ArgumentNullException(nameof(videoService));
        _imageLoaderService = imageLoaderService ?? throw new ArgumentNullException(nameof(imageLoaderService));
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
    /// Handles selection of a media item and prepares the viewer for playback/display.
    /// </summary>
    public async Task SelectMediaItemAsync(MediaItem item)
    {
        if (item == null) return;

        // Reset state first
        _videoService.StopVideoAsync();
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
            await _videoService.PlayVideoAsync(videoItem.FilePath);
            IsViewingVideo = true;
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
            // Use the optimized loader service to get a cached or loaded stream.
            var imageStream = await _imageLoaderService.LoadImageAsync(item.FilePath);

            // In MAUI, ImageSource can often take a Stream directly or require conversion.
            // For simplicity and demonstration of the flow, we'll assume the View/Binding
            // layer handles the stream correctly, but for robustness, we wrap it in a MemoryStream.
            using (var ms = new MemoryStream())
            {
                imageStream.CopyTo(ms);
                ms.Position = 0; // Reset stream position before assignment

                CurrentImageSource = ImageSource.FromStream(() => ms);
                OnPropertyChanged(nameof(CurrentImageSource));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading image for display: {ex.Message}");
            // Optionally set a placeholder error source here
        }
    }

    // INotifyPropertyChanged implementation
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
