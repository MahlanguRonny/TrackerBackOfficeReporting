namespace Trader.Backend.Api.Contracts.Responses
{
    public record TradeCreationResponse
    {
        public int Id { get; set; }
        public int ExternalAccountId { get; set; } = default!;
        public int BatchId { get; set; }
        public int TradeRateId { get; set; }
        public string Symbol { get; set; } = default!;
        public string Side { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime TradeTime { get; set; }
        public string Currency { get; set; } = default!;
    }
}
