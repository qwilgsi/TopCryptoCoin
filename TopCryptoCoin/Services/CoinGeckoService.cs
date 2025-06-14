using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TopCryptoCoin.Models;

namespace TopCryptoCoin.Services
{
    public class CoinGeckoService
    {
        private static readonly HttpClient _http = new HttpClient()
        {
            BaseAddress = new Uri("https://api.coingecko.com/api/v3/")
        };

        static CoinGeckoService()
        {
            _http.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");
        }

        // -----------------------------------------------

        public class CoinTickerResponse
        {
            [JsonPropertyName("tickers")]
            public List<Ticker>? Tickers { get; set; }
        }

        public class Ticker
        {
            [JsonPropertyName("market")]
            public Market? Market { get; set; }

            [JsonPropertyName("trade_url")]
            public string? TradeUrl { get; set; }
        }

        public class Market
        {
            [JsonPropertyName("name")]
            public string? Name { get; set; }
        }

        // -----------------------------------------------

        public async Task<List<Coin>> GetTopCoins(int count = 20)
        {
            var url = $"coins/markets?vs_currency=usd&order=market_cap_desc&per_page={count}&page=1";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Coin>>(stream, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            }) ?? new List<Coin>();
        }

        // -----------------------------------------------

        public async Task<List<CoinMarketPlatform>> GetMarketsForCoin(string coinId)
        {
            var url = $"coins/{coinId}/tickers";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            var tickerData = await JsonSerializer.DeserializeAsync<CoinTickerResponse>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return tickerData?.Tickers?
                .Where(t => !string.IsNullOrEmpty(t.Market?.Name) && !string.IsNullOrEmpty(t.TradeUrl))
                .GroupBy(t => t.Market!.Name)
                .Select(g => new CoinMarketPlatform
                {
                    Name = g.Key,
                    TradeUrl = g.First().TradeUrl ?? ""
                })
                .ToList() ?? new List<CoinMarketPlatform>();
        }
    }
}
