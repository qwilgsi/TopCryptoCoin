using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCryptoCoin.Models
{
    public class CoinMarketPlatform
    {
        public string Name { get; set; } = string.Empty;
        public string TradeUrl { get; set; } = string.Empty;

        public override string ToString() => Name;
    }
}
