using System.Threading;
using System.Timers;
using Android.Widget;
using BHMovie.Model;
using BHMovie.ViewModel;
namespace BHMovie.View;

public partial class MainPage : ContentPage
{
    private readonly MainViewModel _vm;

    private System.Timers.Timer _timer;
    private CancellationTokenSource _cancellationTokenSource;

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

        _timer = new System.Timers.Timer(1000); // Set the interval to 1 second
        _timer.AutoReset = false; // Set AutoReset to false so the timer doesn't repeat
        _timer.Elapsed += OnTimerElapsed;

        _cancellationTokenSource = new CancellationTokenSource();
    }


    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = !Shell.Current.FlyoutIsPresented;
    }

    void Searchbar_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {

        _cancellationTokenSource.Cancel();
        _cancellationTokenSource = new CancellationTokenSource();

        _timer.Stop();
        _timer.Start();
    }


    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        // Check if cancellation was requested
        if (_cancellationTokenSource.Token.IsCancellationRequested)
        {
            return;
        }

        // Call viewmodel search method

        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Shell.Current.DisplayAlert("Error!", $"Unable to  get movies:{Searchbar.Text} ", "OK");
        });
    }


    // Listen for the last element in the MovieCariousel and paginate
    async void MoviesCariusel_CurrentItemChanged(System.Object sender, Microsoft.Maui.Controls.CurrentItemChangedEventArgs e)
    {
        var item = MoviesCariusel.CurrentItem as Movie;

        if (item == _vm.Movies.Last())
        {
            await _vm.GetMoviesByGenreAsync();
        }
    }


    // Listen for genre changes and call appropiate data
    async void GenreList_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        if (!_vm.firstTime)
            await _vm.GetMoviesByGenreAsync();

    }
}


