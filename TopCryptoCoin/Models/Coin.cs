using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopCryptoCoin.Models
{
    public class Coin
    {
        public string? Id { get; set; }
        public string? Sybol { get; set; }
        public string? Name { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal PriceChangingTo24h { get; set; }
    }
}
