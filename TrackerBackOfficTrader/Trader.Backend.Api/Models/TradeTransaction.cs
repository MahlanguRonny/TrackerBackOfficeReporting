using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Tracker.Backend.Domain.Models
{
    [Index(nameof(ExternalAccountId), IsUnique = true)]
    public class TradeTransaction
    {
        [Key]
   
        public int Id { get; set; }
        public int ExternalAccountId { get; set; } = default!;
        public string Symbol { get; set; } = default!;
        public string Side { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TradeTime { get; set; }
        public string Currency { get; set; } = default!;
        public TradeAccount TradeAccount { get; set; }

        public static TradeTransaction Create(int id,int externalAccountId, string symbol, string side, int quantity, decimal price, DateTime tradeTime, string currency)
        {
            var trade = new TradeTransaction
            {
                Id = id,
                ExternalAccountId = externalAccountId,
                Symbol = symbol,
                Side = side,
                Quantity = quantity,
                Price = price,
                TradeTime = tradeTime,
                Currency = currency
            };

            return trade;
        }
    }
}



