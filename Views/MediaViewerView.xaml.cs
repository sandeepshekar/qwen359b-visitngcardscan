using Microsoft.Maui.Controls;
using Qwen359b.ViewModels;
using System.ComponentModel;

namespace Qwen359b.Views;

public partial class MediaViewerView : ContentPage
{
    private readonly MediaViewerViewModel _viewModel;

    public MediaViewerView()
    {
        InitializeComponent();
        _viewModel = (MediaViewerViewModel)BindingContext;
    }
}