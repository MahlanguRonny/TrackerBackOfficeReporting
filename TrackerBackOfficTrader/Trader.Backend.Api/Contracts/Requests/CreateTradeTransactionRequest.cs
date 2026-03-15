namespace Trader.Backend.Api.Contracts.Requests
{
    public record CreateTradeTransactionRequest
    {
        public int ExternalAccountId { get; set; } = default!;
        public string Account { get; set; } = default!;
        public int TradeRateId { get; set; }
        public int BatchId { get; set; }
        public string Symbol { get; set; } = default!;
        public string Side { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TradeTime { get; set; }
        public string Currency { get; set; } = default!;
    }
}
