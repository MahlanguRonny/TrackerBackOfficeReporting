using System;
using System.Linq;
using Tracker.Backend.LegacyService.Data;

namespace Tracker.Backend.LegacyService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "TraderService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select TraderService.svc or TraderService.svc.cs at the Solution Explorer and start debugging.
    public class TraderService : ITraderService
    {
        public TradeRate GetCurrentRateByCurrency(string selectedCurrency)
        {
            TradeRate rate = new TradeRate();
            using (TradeDbEntities tradeDb = new TradeDbEntities())
            {
                rate = tradeDb.TradeRates.FirstOrDefault(x => x.FromCurrency == selectedCurrency);
            }

            return rate;
        }
    }
}
