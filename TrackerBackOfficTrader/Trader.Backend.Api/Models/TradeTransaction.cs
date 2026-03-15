namespace Tracker.Backend.Domain.Models
{
    public class TradeTransaction
    {   
        public int Id { get; set; }
        public int TradeAccountId { get; set; } = default!;
        public int TradeRateId {  get; set; }
        public int BatchId { get; set; }
        public string Symbol { get; set; } = default!;
        public string Side { get; set; } = default!;
        public string Account { get; set; } = default!;   
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TradeTime { get; set; }
        public string Currency { get; set; } = default!;
        //The below object is thee external account
        //TODO rename if time permits
        public TradeAccount TradeAccount { get; set; }

        public static TradeTransaction Create(int externalAccountId, string account, string symbol, string side, int quantity, 
                                                decimal price, DateTime tradeTime, string currency, int tradeRateId, int batchId)
        {
            var trade = new TradeTransaction
            {
                TradeAccountId = externalAccountId,
                Account = account,
                Symbol = symbol,
                Side = side,
                Quantity = quantity,
                Price = price,
                TradeTime = tradeTime,
                Currency = currency,
                TradeRateId = tradeRateId,
                BatchId = batchId
            };

            return trade;
        }
    }
}



