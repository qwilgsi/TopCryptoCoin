using TopCryptoCoin.Models;

namespace TopCryptoCoin.ViewModels
{
    partial class CoinDetailViewModel
    {
        public Coin Coin { get; }

        public CoinDetailViewModel(Coin coin) 
        {
            Coin = coin;
        }
    }
}
