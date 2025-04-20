using System.Diagnostics;
using MauiApp3.Services;
using MauiApp3.Models;

namespace MauiApp3.Views
{
    public partial class BeerDetailPage : ContentPage
{
    private readonly BeerService _beerService;
    private int _beerId;
    private Beer _localBeer;

    public BeerDetailPage(BeerService beerService, int beerId)
    {
        InitializeComponent();
        _beerService = beerService;
        _beerId = beerId;
    }

    public BeerDetailPage(Beer localBeer)
    {
        InitializeComponent();
        _localBeer = localBeer;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (_localBeer != null)
        {
            DisplayLocalBeerDetails();
        }
        else
        {
            await LoadBeerDetailsAsync();
        }
    }

    private void DisplayLocalBeerDetails()
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            if (!string.IsNullOrWhiteSpace(_localBeer.Image))
            {
                BeerImage.Source = ImageSource.FromFile(_localBeer.Image);
            }

            BeerName.Text = _localBeer.Name ?? "Nom inconnu";
            BeerTagline.Text = _localBeer.Price ?? "Prix inconnu";
            BeerDescription.Text = _localBeer.Description ?? "Pas de description disponible";

            BeerRating.Text = "Pas de note disponible"; 
        });
    }

    private async Task LoadBeerDetailsAsync()
    {
        try
        {
            var beer = await _beerService.GetBeerAsync(_beerId);

            Device.BeginInvokeOnMainThread(() =>
            {
                if (!string.IsNullOrWhiteSpace(beer.Image))
                {
                    BeerImage.Source = ImageSource.FromUri(new Uri(beer.Image));
                }

                BeerName.Text = beer.Name ?? "Nom inconnu";
                BeerTagline.Text = beer.Price ?? "Prix inconnu";

                if (beer.Rating != null)
                {
                    BeerRating.Text = $"⭐ {beer.Rating.Average:F1} ({beer.Rating.Reviews} avis)";
                }
                else
                {
                    BeerRating.Text = "Pas de note disponible";
                }
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading beer: {ex}");
            await DisplayAlert("Error", "Failed to load beer details", "OK");
            await Navigation.PopAsync();
        }
    }
}

}