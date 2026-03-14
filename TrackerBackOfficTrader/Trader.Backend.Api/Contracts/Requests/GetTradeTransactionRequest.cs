namespace Trader.Backend.Api.Contracts.Requests
{
    public record GetTradeTransactionRequest
    {
        public DateTime FromDate { get; set; } = DateTime.Now;
        public DateTime ToDate { get; set; } = DateTime.Now;
    }
}
