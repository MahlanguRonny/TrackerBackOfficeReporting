namespace Tracker.Backend.Domain.Models
{
    public class TradeRate
    {
        public int Id { get; set; }
        public string From { get; set; } = default!;
        public string To { get; set; } = default!;
        public decimal RateAmount { get; set; }
        public DateTime AsOfDate { get; set; }

        public static TradeRate Create( string from, string to, decimal rateAmount, DateTime asOfDate)
        {
            var rate = new TradeRate { From = from, To = to, RateAmount = rateAmount, AsOfDate = asOfDate };
            return rate;
        }
    }
}
