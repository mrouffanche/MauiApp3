using MauiApp3.Services;
using MauiApp3.ViewModels;
using MauiApp3.Views;
using System.Net.Http.Headers;

namespace MauiApp3
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            
            builder.Services.AddScoped<BeerService>();
            
            builder.Services.AddSingleton<BeerViewModel>();
            
            builder.Services.AddTransient<BeerListPage>();
            builder.Services.AddTransient<BeerDetailPage>();
            
            builder.Services.AddHttpClient("BeerClient", client => 
            {
                client.BaseAddress = new Uri("https://api.sampleapis.com/");
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            builder.Services.AddScoped<BeerService>();

            return builder.Build();
        }
    }
}