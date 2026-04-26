using Microsoft.Maui.Controls;
using Qwen359b.ViewModels;
using System.Threading.Tasks;

namespace Qwen359b.Views;

public partial class GalleryView : ContentPage
{
    private readonly MediaViewerViewModel _viewModel;

    public GalleryView()
    {
        InitializeComponent();
        // Assuming the BindingContext is set in XAML, we can cast it here for access if needed, 
        // but generally, interaction should go through commands/properties.
        _viewModel = (MediaViewerViewModel)BindingContext;
    }

    private void ToggleGalleryView_Clicked(object sender, EventArgs e)
    {
        // Toggles the view mode using the ViewModel logic.
        if (_viewModel != null)
        {
            _viewModel.ToggleViewMode();
        }
    }

    private void ToggleSelectionMode_Clicked(object sender, EventArgs e)
    {
        // Toggles the view mode using the ViewModel logic.
        if (_viewModel != null)
        {
            _viewModel.ToggleViewMode();
        }
    }

    // Event handler for item selection (assuming CollectionView is configured to handle this)
    private async void OnMediaItemSelected(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Models.MediaItem selectedItem && _viewModel != null)
        {
            // Pass the selected item to the ViewModel for processing based on current mode.
            await _viewModel.SelectMediaItemAsync(selectedItem);
        }
    }
}