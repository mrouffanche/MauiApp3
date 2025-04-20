using Microsoft.Maui.Controls;
using System.Diagnostics;
using MauiApp3.Services;
using MauiApp3.Models;
using MauiApp3.ViewModels;

namespace MauiApp3.Views
{
    public partial class BeerListPage : ContentPage
    {
        
        
        public BeerListPage(BeerViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
    
            try
            {
                await ((BeerViewModel)BindingContext).LoadBeersAsync();
        
                ((BeerViewModel)BindingContext).AddUserAddedBeers();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading beers: {ex}");
            }
        }

        private async void OnBeerSelected(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter != null)
            {
                if (int.TryParse(button.CommandParameter.ToString(), out int beerId))
                {
                    var beerService = Handler.MauiContext?.Services.GetService<BeerService>();
                    if (beerService != null)
                    {
                        var localBeer = BeerStore.UserAddedBeers.FirstOrDefault(b => b.Id == beerId);
                        
                        if (localBeer != null)
                        {
                            await Navigation.PushAsync(new BeerDetailPage(localBeer));
                        }
                        else
                        {
                            await Navigation.PushAsync(new BeerDetailPage(beerService, beerId));
                        }
                    }
                }
            }
        }
    }
}
