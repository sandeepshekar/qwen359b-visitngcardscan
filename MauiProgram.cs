using Microsoft.Maui;
using Microsoft.Extensions.DependencyInjection;
using Qwen359b.Controls;
using Qwen359b.Models;
using Qwen359b.Services;
using Qwen359b.Services.PlatformHandlers;
using Qwen359b.ViewModels;
using Qwen359b.Views;

namespace Qwen359bVisitingCardScan;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Register services with dependency injection
        builder.Services.AddSingleton<IMediaService, MediaScannerService>();
        builder.Services.AddSingleton<IImageLoaderService, ImageLoadingService>();
        builder.Services.AddSingleton<IVideoService, VideoService>();
        
        // Platform-specific player handler registration
        #if WINDOWS
        builder.Services.AddSingleton<IPlatformMediaPlayer, WindowsMediaPlayerHandler>();
        #elif ANDROID
        builder.Services.AddSingleton<IPlatformMediaPlayer, AndroidMediaPlayerHandler>();
        #elif IOS
        builder.Services.AddSingleton<IPlatformMediaPlayer, iOSMediaPlayerHandler>();
        #endif

        // Register ViewModels
        builder.Services.AddTransient<MediaViewerViewModel>();
        builder.Services.AddTransient<GalleryViewModel>();

        // Register Views
        builder.Services.AddTransient<GalleryView>();
        builder.Services.AddTransient<MediaViewerView>();

        // Register Controls
        builder.Services.AddTransient<CustomVideoPlayerControl>();

        return builder.Build();
    }
}
