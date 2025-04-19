using System.Diagnostics;
using MauiApp3.Services;
using MauiApp3.Models;

namespace MauiApp3.Views
{
    public partial class AddBeerPage : ContentPage
    {
        public AddBeerPage()
        {
            InitializeComponent();
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            var newBeer = new Beer
            {
                Name = TitleEntry.Text,
                Price = DescriptionEntry.Text,
                Image = ImageEntry.Text,
                Rating = new Rating { Average = 0, Reviews = 0 }
            };

            BeerStore.UserAddedBeers.Add(newBeer);
            await DisplayAlert("Succès", "Bière ajoutée !", "OK");
            await Navigation.PopAsync(); 
        }
    }
}