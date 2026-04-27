using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using Qwen359b.Controls;
using Qwen359b.Services;
using Qwen359b.Services.PlatformHandlers;

namespace Qwen359bVisitingCardScan;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        
        builder
            .UseMauiCommunityToolkit()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorHub();
        builder.WebBuilder.EnableBlazorWebAssemblyLoadPathCompression = true;

        // Register services with dependency injection
        builder.Services.AddSingleton<IMediaService, MediaScannerService>();
        builder.Services.AddSingleton<IImageLoadingService, ImageLoadingService>();
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
        builder.Services.AddTransient<ViewModels.MediaViewerViewModel>();
        builder.Services.AddTransient<ViewModels.GalleryViewModel>();

        // Register Views
        builder.Services.AddTransient<Views.GalleryView>();
        builder.Services.AddTransient<Views.MediaViewerView>();

        // Register Controls
        builder.Services.AddTransient<Controls.CustomVideoPlayerControl>();

        return builder.Build();
    }
}
