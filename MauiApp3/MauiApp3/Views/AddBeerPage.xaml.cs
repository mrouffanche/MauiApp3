using MauiApp3.Models;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

namespace MauiApp3.Views
{
    public partial class AddBeerPage : ContentPage
    {
        private string _imagePath;

        public AddBeerPage()
        {
            InitializeComponent();
        }

        private async void OnPickImageClicked(object sender, EventArgs e)
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.Photos>();
                if (status != PermissionStatus.Granted)
                    return;

                var result = await MediaPicker.PickPhotoAsync();
                if (result != null)
                {
                    _imagePath = result.FullPath;
                    SelectedImage.Source = ImageSource.FromFile(_imagePath);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erreur", ex.Message, "OK");
            }
        }

        private async void OnAddClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleEntry.Text))
            {
                await DisplayAlert("Erreur", "Le nom est obligatoire", "OK");
                return;
            }

            var newBeer = new Beer
            {
                Name = TitleEntry.Text,
                Price = DescriptionEntry.Text,
                Description = DescriptionEntry.Text, 
                Image = _imagePath,
                Id = BeerStore.UserAddedBeers.Count + 1000
            };

            BeerStore.UserAddedBeers.Add(newBeer);
            await DisplayAlert("Succès", "Bière ajoutée !", "OK");
            await Navigation.PopAsync();
        }
    }
}