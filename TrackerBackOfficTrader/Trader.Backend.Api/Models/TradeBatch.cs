using Microsoft.VisualBasic;

namespace Trader.Backend.Api.Models
{
    public class TradeBatch
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }

        public static TradeBatch Create()
        {
            var tradeBatch = new TradeBatch
            {
                DateCreated = DateTime.Now,
            };

            return tradeBatch;
        }
    }
}
