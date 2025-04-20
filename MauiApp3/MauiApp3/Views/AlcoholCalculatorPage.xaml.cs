using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MauiApp3.Views
{
    public partial class AlcoholCalculatorPage : ContentPage, INotifyPropertyChanged
    {
        private string _newTask;
        public ObservableCollection<string> TaskList { get; } = new ObservableCollection<string>();

        public string NewTask
        {
            get => _newTask;
            set
            {
                _newTask = value;
                OnPropertyChanged();
            }
        }
        public Command AddTaskCommand { get; }

    
        public AlcoholCalculatorPage()
        {
            InitializeComponent();
            
            AddTaskCommand = new Command(ExecuteAddTask);


            BindingContext = this;

            BeerCountSlider.ValueChanged += OnSliderValueChanged;
            AlcoholDegreeSlider.ValueChanged += OnSliderValueChanged;
        }

        private void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            OnPropertyChanged(nameof(AlcoholResult));
            OnPropertyChanged(nameof(EstimatedSoberTime));
            OnPropertyChanged(nameof(CaloriesResult));
        }


        public string AlcoholResult
        {
            get
            {
                double volumeCl = 25;
                int beerCount = (int)Math.Round(BeerCountSlider.Value);
                double totalAlcohol = beerCount * volumeCl * AlcoholDegreeSlider.Value / 10 * 0.8;
                return $"{totalAlcohol:F1}g d'alcool pur";
            }
        }

        public string EstimatedSoberTime
        {
            get
            {
                double volumeCl = 25; 
                double alcoholDegree = AlcoholDegreeSlider.Value; 
                double beerCount = Math.Round(BeerCountSlider.Value);

                double totalAlcohol = beerCount * volumeCl * alcoholDegree / 10 * 0.8;

                double userWeight = 70;

                double eliminationRatePerKgPerHour = 0.1;

                double eliminationTimeInHours = totalAlcohol / (userWeight * eliminationRatePerKgPerHour);

                if (eliminationTimeInHours <= 0)
                    return "Tu es déjà sobre 🍃";

                int wholeHours = (int)Math.Ceiling(eliminationTimeInHours);

                return $"Temps estimé pour être sobre : ~{wholeHours}h ⏳";
            }
        }
        
        public string CaloriesResult
        {
            get
            {
                double volumeCl = 25; 
                double alcoholDegree = AlcoholDegreeSlider.Value; 
                double caloriesPer100ml = 43; 
                double alcoholCalories = BeerCountSlider.Value * volumeCl * alcoholDegree / 10 * 7;

                double otherCalories = BeerCountSlider.Value * volumeCl * caloriesPer100ml / 100;

                double totalCalories = alcoholCalories + otherCalories;

                return $"{totalCalories:F0} kcal";
            }
        }
        
        private void ExecuteAddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTask))
            {
                TaskList.Add(NewTask);
                NewTask = string.Empty;
            }
        }
        
        private void OnAddTask(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(NewTask))
            {
                TaskList.Add(NewTask);
                NewTask = string.Empty;
                OnPropertyChanged(nameof(NewTask));
            }
        }

        private void OnDeleteTask(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is string task)
            {
                TaskList.Remove(task);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}