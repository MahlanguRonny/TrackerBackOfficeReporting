namespace Tracker.Backend.Domain.Models
{
    public class TradeRate
    {
        public int Id { get; set; }
        public string FromCurrency { get; set; } = default!;
        public string ToCurrency { get; set; } = default!;
        public decimal RateAmount { get; set; }
        public DateTime AsOfDate { get; set; }

        public static TradeRate Create( string fromCurrency, string toCurrency, decimal rateAmount, DateTime asOfDate)
        {
            var rate = new TradeRate { FromCurrency = fromCurrency, ToCurrency = toCurrency, RateAmount = rateAmount, AsOfDate = asOfDate };
            return rate;
        }
    }
}
