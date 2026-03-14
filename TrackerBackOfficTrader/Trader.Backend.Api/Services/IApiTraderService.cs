namespace Trader.Backend.Api.Services
{
    public interface IApiTraderService
    {
        Task<TradeCreationResponse> AddTradeTransactionAsync(CreateTradeTransactionRequest createTradeTransaction);
        Task<IEnumerable<TradeTransationResponse>> TradeTransactionsByDate(GetTradeTransactionRequest tradeTransactionRequest);
    }
}
