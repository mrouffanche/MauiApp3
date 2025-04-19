using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MauiApp3.Models;
using MauiApp3.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http;

namespace MauiApp3.ViewModels
{
    public class BeerViewModel : INotifyPropertyChanged
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();

                    OnPropertyChanged(nameof(IsNotBusy));
                }
            }
        }

        public bool IsNotBusy => !IsBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly BeerService _beerService;
        public ObservableCollection<Beer> Beers { get; set; }

        public BeerViewModel(BeerService beerService)
        {
            _beerService = beerService;
            Beers = new ObservableCollection<Beer>();
        }

        public async Task LoadBeersAsync()
        {
            try
            {
                IsBusy = true;

                var beers = await _beerService.GetBeersAsync();

                Beers.Clear();
                foreach (var beer in beers.Take(50))
                {
                    Beers.Add(beer);
                }


            }
            catch (HttpRequestException httpEx)
            {
                await Shell.Current.DisplayAlert("Erreur réseau", 
                    $"Impossible de contacter l'API: {httpEx.Message}", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erreur", 
                    $"Problème avec les données: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
