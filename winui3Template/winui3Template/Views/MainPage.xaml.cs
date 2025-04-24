using Microsoft.UI.Xaml.Controls;

using winui3Template.ViewModels;

namespace winui3Template.Views;

public sealed partial class MainPage : Page
{
    public MainViewModel ViewModel
    {
        get;
    }

    public MainPage()
    {
        ViewModel = App.GetService<MainViewModel>();
        InitializeComponent();
    }
}
