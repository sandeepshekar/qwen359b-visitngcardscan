using System.Windows.Controls;
using Qwen359b.ViewModels;

namespace Qwen359b.Views;

public partial class MediaViewerView : Page
{
    private readonly MediaViewerViewModel _viewModel;

    public MediaViewerView()
    {
        InitializeComponent();
        _viewModel = new MediaViewerViewModel(null, null, null);
        DataContext = _viewModel;
    }

    private void GoBack_Clicked(object sender, System.Windows.RoutedEventArgs e)
    {
        NavigationService?.GoBack();
    }
}