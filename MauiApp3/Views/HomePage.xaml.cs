using Microsoft.Maui;
using Microsoft.Maui.Controls;
using MauiApp3.Views;

namespace MauiApp3.Views;

    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnGifButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GifPage());
        }
    }
