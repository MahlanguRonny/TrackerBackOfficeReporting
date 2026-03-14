using Trader.Backend.Api.AppContext;
using Trader.Backend.Api.Contracts.Requests;
using Trader.Backend.Api.Contracts.Responses;

namespace Trader.Backend.Api.Services
{
    public class TraderService : ITrader
    {
        private readonly TraderAppContext _context;
        private readonly ILogger<TraderService> _logger;

        public TraderService(TraderAppContext context, ILogger<TraderService> logger)
        {
            _context = context;
            _logger = logger;
        }
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
