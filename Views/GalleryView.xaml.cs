using System.Windows.Controls;
using Qwen359b.ViewModels;

namespace Qwen359b.Views;

public partial class GalleryView : Page
{
    private readonly MediaViewerViewModel _viewModel;

    public GalleryView()
    {
        InitializeComponent();
        _viewModel = new MediaViewerViewModel(null, null, null);
        DataContext = _viewModel;
    }

    private void ToggleGalleryView_Clicked(object sender, System.Windows.RoutedEventArgs e)
    {
        // Toggle gallery view mode
        if (_viewModel != null)
        {
            _viewModel.IsViewingAllMode = !_viewModel.IsViewingAllMode;
        }
    }

    private void ToggleSelectionMode_Clicked(object sender, System.Windows.RoutedEventArgs e)
    {
        // Toggle selection mode
        if (_viewModel != null)
        {
            // Toggle selection mode logic
        }
    }
}