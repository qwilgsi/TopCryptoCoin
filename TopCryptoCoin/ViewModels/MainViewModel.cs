using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TopCryptoCoin.Services;

namespace TopCryptoCoin.ViewModels
{
    partial class MainPage : Page
    {
        private readonly CoinGeckoService _service = new CoinGeckoService();
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            await AddToCoinsListView();
        }

        async Task AddToCoinsListView()
        {
            var result = await _service.GetTopCoins();
            foreach (var coin in result) 
            {
                ListViewCoins.Items.Add(coin);
            }
        }
    }
}
