using Microsoft.Maui.Controls;
using Qwen359b.Models;
using Qwen359b.Services;
using Qwen359b.ViewModels;

namespace Qwen359b.Views;

public partial class GalleryView : ContentPage
{
    private readonly GalleryViewModel _viewModel;
    private List<string> _selectedDirectories = new List<string>();
    private List<string> _defaultDirectories = new List<string>();

    public GalleryView(GalleryViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        // Load media when the page appears
        if (_selectedDirectories.Count == 0)
        {
            await _viewModel.LoadMediaAsync(_defaultDirectories);
        }
    }

    private void ToggleGalleryView_Clicked(object sender, EventArgs e)
    {
        // Toggles the view mode using the ViewModel logic.
        _viewModel?.ToggleViewMode();
    }

    private void ToggleSelectionMode_Clicked(object sender, EventArgs e)
    {
        // Toggles the view mode using the ViewModel logic.
        _viewModel?.ToggleViewMode();
    }

    // Event handler for item selection (assuming CollectionView is configured to handle this)
    private async void OnMediaItemSelected(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is MediaItem selectedItem && _viewModel != null)
        {
            // Pass the selected item to the ViewModel for processing based on current mode.
            await _viewModel.SelectMediaItemAsync(selectedItem);
        }
    }
}