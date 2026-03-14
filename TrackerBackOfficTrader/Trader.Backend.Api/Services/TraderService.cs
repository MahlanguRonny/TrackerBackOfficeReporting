using Trader.Backend.Api.Contracts.Requests;
using Trader.Backend.Api.Contracts.Responses;

namespace Trader.Backend.Api.Services
{
    public class TraderService : ITrader
    {
        public Task<TradeTransationResponse> AddTradeTransaction(CreateTradeTransactionRequest createTradeTransaction)
        {
            throw new NotImplementedException();
        }

        public Task<TradeTransationResponse> TradeTransactionsByDate(GetTradeTransactionRequest tradeTransactionRequest)
        {
            throw new NotImplementedException();
        }
    }
}
