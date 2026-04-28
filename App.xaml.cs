using Microsoft.Maui.Controls;
using Qwen359b.Services;
using Qwen359b.ViewModels;
using Qwen359b.Views;

namespace Qwen359bVisitingCardScan;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        var mediaService = (IMediaService)Application.Current.Services.GetRequiredService(typeof(IMediaService));
        var imageLoaderService = (IImageLoaderService)Application.Current.Services.GetRequiredService(typeof(IImageLoaderService));
        
        var galleryViewModel = new GalleryViewModel(mediaService, imageLoaderService);
        
        MainPage = new GalleryView(galleryViewModel);
    }
}
