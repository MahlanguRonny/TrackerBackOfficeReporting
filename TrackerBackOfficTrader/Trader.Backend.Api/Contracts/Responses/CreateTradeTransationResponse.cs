namespace Trader.Backend.Api.Contracts.Responses
{
    public record CreateTradeTransationResponse
    {
        public int ExternalAccountId { get; set; }
        public string Account {  get; set; } = default!;
        public int TotalQuantuty { get; set; }
        public decimal AvaragePrice { get; set; }
        public double NotionalBase { get; set; }
        public string BaseCurrency { get; set; } = default!;

    }
}
