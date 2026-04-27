using Microsoft.Maui.Controls;
using Qwen359b.ViewModels;

namespace Qwen359b.Views;

public partial class MediaViewerView : ContentPage
{
    private readonly MediaViewerViewModel _viewModel;

    public MediaViewerView(MediaViewerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Handle media playback when the page appears
        if (_viewModel != null && _viewModel.IsViewingVideo)
        {
            await _viewModel.SelectMediaItemAsync(_viewModel.SelectedMediaItem);
        }
    }

    private async void GoBack_Clicked(object sender, EventArgs e)
    {
        // Navigate back to gallery
        await Navigation.PushAsync(new GalleryView(
            (MediaViewerViewModel)DependencyService.Get<ViewModels.MediaViewerViewModel>()));
    }
}