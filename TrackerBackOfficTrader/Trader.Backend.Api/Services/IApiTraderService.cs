namespace Trader.Backend.Api.Services
{
    public interface IApiTraderService
    {
        Task<TradeCreationResponse> AddTradeTransactionAsync(CreateTradeTransactionRequest createTradeTransaction);
        Task<IEnumerable<TradeTransationResponse>> TradeTransactionsByDate(GetTradeTransactionRequest tradeTransactionRequest);
        Task<IEnumerable<TradeCreationResponse>> AddBatchTradeTransaction(List<CreateTradeTransactionRequest> tradeTransactionBatch);
        Task<IEnumerable<TradeTransationResponse>> BatchTradeTransactionsByBatchId(int batchId);
    }
}
