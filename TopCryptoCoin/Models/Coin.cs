using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TopCryptoCoin.Models
{
    public class Coin
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("current_price")]  
        public decimal CurrentPrice { get; set; }

        [JsonPropertyName("price_change_percentage_24h")]
        public decimal PriceChangingTo24h { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Symbol}) - ${CurrentPrice}";
        }
    }
}
