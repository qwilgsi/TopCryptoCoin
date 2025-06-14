using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using TopCryptoCoin.Models;
using TopCryptoCoin.Services;

namespace TopCryptoCoin.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public Frame MainFrame { get; }

        private readonly CoinGeckoService _service = new();
        private ObservableCollection<Coin> _allCoins = new();

        public ObservableCollection<Coin> Coins { get; set; } = new();
        private Coin _selectedCoin;
        public Coin SelectedCoin
        {
            get => _selectedCoin;
            set
            {
                _selectedCoin = value;
                OnPropertyChanged();
            }
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SearchCommand { get; }
        public ICommand ResetCommand { get; }

        // -----------------------------------------------

        public MainViewModel(Frame mainFrame)
        {
            MainFrame = mainFrame;
            SearchCommand = new RelayCommand(ExecuteSearch);
            ResetCommand = new RelayCommand(ExecuteReset);
            LoadCoins();
        }

        // -----------------------------------------------

        private async void LoadCoins()
        {
            var result = await _service.GetTopCoins();
            _allCoins = new ObservableCollection<Coin>(result);
            Coins = _allCoins;
            OnPropertyChanged(nameof(Coins));
        }

        private void ExecuteSearch()
        {
            var filtered = _allCoins
                .Where(c => c.Name?.ToLower() == SearchText.ToLower() || 
                            c.Symbol?.ToLower() == SearchText.ToLower()) 
                .ToList();
            
            Coins = new ObservableCollection<Coin>(filtered);
            OnPropertyChanged(nameof(Coins));
        }

        private void ExecuteReset()
        {
            Coins = _allCoins;
            OnPropertyChanged(nameof(Coins));
            SearchText = string.Empty;
            OnPropertyChanged(nameof(SearchText));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
