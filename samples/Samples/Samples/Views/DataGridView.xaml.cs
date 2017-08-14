using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FormsGtkToolkit.Samples.Views
{
    public partial class DataGridView : ContentPage
    {
        public DataGridView()
        {
            InitializeComponent();

            BindingContext = new DataGridViewModel();
        }
    }

    public class Standing
    {
        public int Position { get; set; }
        public string Constructor { get; set; }
        public string Nationality { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
    }

    public class DataGridViewModel : BindableObject
    {
        private ObservableCollection<Standing> _constructorStandings;

        public DataGridViewModel()
        {
            ConstructorStandings = new ObservableCollection<Standing>
            {
                new Standing { Position = 1, Constructor = "Mercedes", Nationality = "German", Points = 250, Wins = 4 },
                new Standing { Position = 2, Constructor = "Ferrari", Nationality = "Italian", Points = 226, Wins = 3 },
                new Standing { Position = 3, Constructor = "Red Bull", Nationality = "Austrian", Points = 137, Wins = 1 },
                new Standing { Position = 4, Constructor = "Force India", Nationality = "Indian", Points = 79, Wins = 0 },
                new Standing { Position = 5, Constructor = "Williams", Nationality = "British", Points = 37, Wins = 0 },
                new Standing { Position = 6, Constructor = "Toro Rosso", Nationality = "Italian", Points = 33, Wins = 0 },
                new Standing { Position = 7, Constructor = "Haas F1 Team", Nationality = "American", Points = 21, Wins = 0 },
                new Standing { Position = 8, Constructor = "Renault", Nationality = "French", Points = 18, Wins = 0 },
                new Standing { Position = 9, Constructor = "Sauber", Nationality = "Swiss", Points = 5, Wins = 0 },
                new Standing { Position = 10, Constructor = "McLaren", Nationality = "British", Points = 2, Wins = 0 }
            };
        }

        public ObservableCollection<Standing> ConstructorStandings
        {
            get { return _constructorStandings; }
            set
            {
                _constructorStandings = value;
                OnPropertyChanged();
            }
        }
    }
}