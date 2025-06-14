using System.Windows.Input;
using TopCryptoCoin.Models;
using System.Diagnostics;
using TopCryptoCoin.Services;

namespace TopCryptoCoin.ViewModels
{
    partial class CoinDetailViewModel
    {
        public Coin Coin { get; }

        private readonly CoinGeckoService _service = new();

        public ICommand OpenMarketCommand { get; }
        public ICommand OpenMarketPlatformCommand { get; }
        
        // -----------------------------------------------

        public CoinDetailViewModel(Coin coin)
        {
            Coin = coin;
            OpenMarketCommand = new RelayCommand<string>(OpenMarket);
            OpenMarketPlatformCommand = new RelayCommand<object>(OpenMarketPlatform);
            LoadMarkets();
        }

        // -----------------------------------------------

        private void OpenMarket(string? url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
        }

        private async void LoadMarkets()
        {
            var platforms = await _service.GetMarketsForCoin(Coin.Id ?? "");
            Coin.MarketPlatforms.Clear();
            foreach (var m in platforms)
                Coin.MarketPlatforms.Add(m);
        }

        private void OpenMarketPlatform(object? param)
        {
            if (param is CoinMarketPlatform platform && !string.IsNullOrWhiteSpace(platform.TradeUrl))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = platform.TradeUrl,
                    UseShellExecute = true
                });
            }
        }
    }
}
