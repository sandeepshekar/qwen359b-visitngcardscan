using System.Windows.Controls;
using Qwen359b.Services;

namespace Qwen359b.Controls;

public partial class CustomVideoPlayerControl : UserControl
{
    private readonly IPlatformMediaPlayer _player;
    private readonly IImageLoadingService _imageLoaderService;

    public CustomVideoPlayerControl()
    {
        InitializeComponent();
        // TODO: Initialize with dependency injection
        // _player = serviceProvider.GetService<IPlatformMediaPlayer>();
        // _imageLoaderService = serviceProvider.GetService<IImageLoadingService>();
    }
}