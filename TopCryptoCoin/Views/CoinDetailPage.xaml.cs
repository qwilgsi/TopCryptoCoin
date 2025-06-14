using System.Windows.Controls;
using System.Windows.Input;
using TopCryptoCoin.Models;
using TopCryptoCoin.ViewModels;

namespace TopCryptoCoin.Views
{
    public partial class CoinDetailPage : Page
    {
        public CoinDetailPage(Coin coin)
        {
            InitializeComponent();
            DataContext = new CoinDetailViewModel(coin);
        }

        private void MarketListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is CoinMarketPlatform platform)
            {
                if (DataContext is CoinDetailViewModel vm)
                {
                    vm.OpenMarketPlatformCommand.Execute(platform);
                }
            }
        }
    }
}
