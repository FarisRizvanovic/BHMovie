using Android.Widget;
using BHMovie.ViewModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;


namespace BHMovie.View;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _vm;


    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _vm.LoadDataAsync();

    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}


