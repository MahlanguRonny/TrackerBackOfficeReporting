using Microsoft.EntityFrameworkCore;
using Tracker.Backend.Domain.Models;
using Trader.Backend.Api.AppContext;
using Trader.Backend.Api.Contracts.Requests;
using Trader.Backend.Api.Contracts.Responses;

namespace Trader.Backend.Api.Services
{
    public class ApiTraderService : IApiTraderService
    {
        private readonly TraderAppContext _context;
        private readonly ILogger<ApiTraderService> _logger;

        public ApiTraderService(TraderAppContext context, ILogger<ApiTraderService> logger)
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

                _context.Add(trade);
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Trade for account {trade.TradeAccountId} created successfully");

                return new TradeCreationResponse
                {
                    ExternalAccountId = trade.TradeAccountId,
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

        public async Task<IEnumerable<TradeTransationResponse>> TradeTransactionsByDate(GetTradeTransactionRequest tradeTransactionRequest)
        {
            try
            {
                var startDate = tradeTransactionRequest.FromDate;
                var endDate = tradeTransactionRequest.ToDate;

                var result = await _context.TradeTransactions
                    .Where(tt => tt.TradeTime >= startDate && tt.TradeTime <= endDate)
                    .GroupBy(tt => new { tt.Account, tt.Symbol })
                    .Select(g => new
                    {
                        Account = g.Key.Account,
                        Symbol = g.Key.Symbol,
                        Total_Quantity = g.Sum(x => x.Quantity),
                        Average_Price = g.Average(x => x.Price),
                        Notional_Base = g.Sum(x => x.Quantity) * g.Average(x => x.Price),
                    })
                    .ToListAsync();

                return result.Select(trade => new TradeTransationResponse
                {
                    Account = trade.Account,                    
                    AvaragePrice = trade.Average_Price,
                    TotalQuantity = trade.Total_Quantity,
                    NotionalBase = (double)trade.Notional_Base,
                    Symbol = trade.Symbol
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while fetching trading report: {ex.Message}");
                throw;
            }
        }
    }
}
