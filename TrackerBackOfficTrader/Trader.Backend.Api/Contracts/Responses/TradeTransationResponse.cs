namespace Trader.Backend.Api.Contracts.Responses
{
    public record TradeTransationResponse
    {
        public string Account {  get; set; } = default!;
        public string Symbol { get; set; } = default!;
        public int TotalQuantity { get; set; }
        public decimal AvaragePrice { get; set; }
        public double NotionalBase { get; set; }
        public string BaseCurrency { get; set; } = default!;

    }
}
