using Tracker.Backend.Domain.Models;
using Trader.Backend.Api.AppContext;
using Trader.Backend.Api.Contracts.Requests;
using Trader.Backend.Api.Contracts.Responses;

namespace Trader.Backend.Api.Services
{
    public class TraderService : ITraderService
    {
        private readonly TraderAppContext _context;
        private readonly ILogger<TraderService> _logger;

        public TraderService(TraderAppContext context, ILogger<TraderService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<TradeCreationResponse> AddTradeTransactionAsync(CreateTradeTransactionRequest createTradeTransaction)
        {
            try
            {
                var trade = TradeTransaction.Create(createTradeTransaction.ExternalAccountId, createTradeTransaction.Symbol,
                                                        createTradeTransaction.Side, createTradeTransaction.Quantity, createTradeTransaction.Price,
                                                         createTradeTransaction.TradeTime, createTradeTransaction.Currency, createTradeTransaction.TradeRateId
                                                    );
                //var trade = new TradeTransaction
                //{
                //    Currency = createTradeTransaction.Currency,
                //    ExternalAccountId = createTradeTransaction.ExternalAccountId,
                //    Price = createTradeTransaction.Price,
                //    Quantity = createTradeTransaction.Quantity,
                //    Side = createTradeTransaction.Side,
                //    Symbol = createTradeTransaction.Symbol,
                //    TradeTime = createTradeTransaction.TradeTime,
                //};

                _context.Add(trade);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Trade for account {trade.ExternalAccountId} created successfully");

                return new TradeCreationResponse
                {
                    ExternalAccountId = trade.ExternalAccountId,
                    Currency = trade.Currency,
                    Id = trade.Id,
                    Price = trade.Price,
                    Quantity = trade.Quantity,
                    Side = trade.Side,
                    Symbol = trade.Symbol,
                    TradeTime = trade.TradeTime,
                    TradeRateId = trade.TradeRateId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Account trading error: {ex.Message}");
                throw;
            }
        }

        public Task<TradeTransationResponse> TradeTransactionsByDate(GetTradeTransactionRequest tradeTransactionRequest)
        {
            throw new NotImplementedException();
        }
    }
}
