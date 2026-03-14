using Trader.Backend.Api.Contracts.Requests;
using Trader.Backend.Api.Contracts.Responses;

namespace Trader.Backend.Api.Services
{
    public interface ITraderService
    {
        Task<TradeCreationResponse> AddTradeTransactionAsync(CreateTradeTransactionRequest createTradeTransaction);
        Task<TradeTransationResponse> TradeTransactionsByDate(GetTradeTransactionRequest tradeTransactionRequest);
    }
}
