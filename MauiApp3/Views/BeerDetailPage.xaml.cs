using System.Diagnostics;
using MauiApp3.Services;

namespace MauiApp3.Views
{
    public partial class BeerDetailPage : ContentPage
    {
        private readonly BeerService _beerService;
        private int _beerId;

        public BeerDetailPage(BeerService beerService, int beerId)
        {
            InitializeComponent();
            _beerService = beerService;
            _beerId = beerId;
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();
    
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
        } }
}