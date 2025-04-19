using Microsoft.Maui.Controls;

namespace MauiApp3.Views;

    public partial class GifPage : ContentPage
    {
        public GifPage()
        {
            InitializeComponent();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
