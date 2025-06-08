using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

        public async Task<List<Coin>> GetTopCoins(int count = 20)
        {
            var url = $"coins/markets?vs_currency=usd&order=market_cap_desc&per_page={count}&page=1";
            var response = await _http.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {content}");
            }

            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<Coin>>(stream, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            }) ?? new List<Coin>();
        }
    }
}
