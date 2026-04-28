using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Qwen359b.Models;
using Qwen359b.Services;

namespace Qwen359b.ViewModels;

public class GalleryViewModel : INotifyPropertyChanged
{
    private readonly IMediaService _mediaService;
    private readonly IImageLoaderService _imageLoaderService;

    // Properties for MVVM binding
    public ObservableCollection<MediaItem> MediaItems { get; set; } = new ObservableCollection<MediaItem>();
    public MediaItem? SelectedMediaItem { get; set; }
    public string ThumbnailSource => SelectedMediaItem?.ThumbnailSource ?? string.Empty;
    public string FileName => SelectedMediaItem?.FileName ?? string.Empty;
    
    // Properties for source generator binding (x:Name attributes)
    public object? ThumbnailImage => SelectedMediaItem?.ThumbnailSource;

    // State for View Mode (View All vs Select Individual Item)
    private bool _isSelectingMode = true;
    public bool IsViewingAllMode 
    { 
        get => !_isSelectingMode; 
        set
        {
            if (_isSelectingMode != value)
            {
                _isSelectingMode = !value;
                OnPropertyChanged(nameof(IsViewingAllMode));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public GalleryViewModel(IMediaService mediaService, IImageLoaderService imageLoaderService)
    {
        _mediaService = mediaService ?? throw new ArgumentNullException(nameof(mediaService));
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

        MediaItems.Clear();
        await ScanAndLoadMediaAsync();
    }

    private async Task ScanAndLoadMediaAsync()
    {
        try
        {
            var items = await _mediaService.ScanDirectoriesAsync(_selectedDirectories);
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

    private List<string> _selectedDirectories = new List<string>();

    public void ToggleViewMode()
    {
        IsViewingAllMode = !IsViewingAllMode;
    }

    public async Task SelectMediaItemAsync(MediaItem item)
    {
        if (item == null) return;

        SelectedMediaItem = item;
        OnPropertyChanged(nameof(SelectedMediaItem));
        OnPropertyChanged(nameof(ThumbnailSource));
        OnPropertyChanged(nameof(FileName));
    }

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
