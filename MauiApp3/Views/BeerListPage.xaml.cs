using Microsoft.Maui.Controls;
using MauiApp3.ViewModels;
using MauiApp3.Services;

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
                
            }
            catch
            {
                
            }
        }



        private async void OnBeerSelected(object sender, EventArgs e)
        {
            if (sender is Button button && int.TryParse(button.CommandParameter?.ToString(), out int beerId))
            {
                var beerService = Handler.MauiContext?.Services.GetService<BeerService>();
                if (beerService != null)
                {
                    await Navigation.PushAsync(new BeerDetailPage(beerService, beerId));
                }
            }
        }
    }
}