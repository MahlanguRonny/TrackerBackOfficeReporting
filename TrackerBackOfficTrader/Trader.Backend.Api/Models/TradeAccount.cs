
namespace Tracker.Backend.Domain.Models
{
    public class TradeAccount
    {
        public int TradeAccountId { get; set; }
        public string Name { get; set; } = default!;
        public ICollection<TradeTransaction> TradeTransactions { get; set; }

        public static TradeAccount Create(int id, string name)
        {
            var account = new TradeAccount { TradeAccountId = id , Name = name };
            return account;
        }
    }
}
