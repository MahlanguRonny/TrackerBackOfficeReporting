namespace Trader.Backend.Api.Exceptions
{
    public class TradeDoesNotExistException(DateTime fromDate, DateTime toDate) : Exception($"No trade record from: {fromDate} to {toDate}")
    {
        private DateTime fromDate { get; set; } = fromDate;
        private DateTime toDate { get; set; } = toDate;
    }
}
