using System.Windows.Controls;
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
    }
}
