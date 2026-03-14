
namespace Tracker.Backend.Domain.Models
{
    public class TradeAccount
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public static TradeAccount Create(int id, string name)
        {
            var account = new TradeAccount { Id = id , Name = name };
            return account;
        }
    }
}
