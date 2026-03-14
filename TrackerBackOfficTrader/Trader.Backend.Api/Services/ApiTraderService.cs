using Microsoft.EntityFrameworkCore;
using Trader.Backend.Api.AppContext;
using WcfServiceReference;

namespace Trader.Backend.Api.Services
{
    public class ApiTraderService : IApiTraderService
    {
        private readonly TraderAppContext _context;
        private readonly ILogger<ApiTraderService> _logger;
        private readonly ITraderService _traderService;

        public ApiTraderService(TraderAppContext context, ILogger<ApiTraderService> logger, ITraderService traderService)
        {
            _context = context;
            _logger = logger;
            _traderService = traderService;
        }

        public async Task<TradeCreationResponse> AddTradeTransactionAsync(CreateTradeTransactionRequest createTradeTransaction)
        {
            TradeCreationResponse tradeCreationResponse = new TradeCreationResponse();

            //get the rate details from the wcf and only there's data return we can continue with the processing
            var rateData = await _traderService.GetCurrentRateByCurrencyAsync(createTradeTransaction.Currency);
            if (rateData is not null)
            {
                var externalAccountDetails = await _context.RateAccounts.FirstAsync(x => x.TradeAccountId == createTradeTransaction.ExternalAccountId);
                if (externalAccountDetails is not null)
                {
                    var baseCurrenyAmount = createTradeTransaction.Price * rateData.RateAmount;
                    var trade = TradeTransaction.Create(externalAccountDetails.TradeAccountId, createTradeTransaction.Account, createTradeTransaction.Symbol,
                                                            createTradeTransaction.Side, createTradeTransaction.Quantity, baseCurrenyAmount,
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
            }

            return tradeCreationResponse;
        }

        public async Task<IEnumerable<TradeTransationResponse>> TradeTransactionsByDate(GetTradeTransactionRequest tradeTransactionRequest)
        {
            var startDate = tradeTransactionRequest.FromDate;
            var endDate = tradeTransactionRequest.ToDate;

#pragma warning disable IDE0037 // Use inferred member name
            var result = await _context.TradeTransactions
                .Where(tt => tt.TradeTime >= startDate && tt.TradeTime <= endDate)
                .GroupBy(tt => new { tt.Account, tt.Symbol, tt.Currency })
                .Select(g => new
                {
                    Account = g.Key.Account,
                    Symbol = g.Key.Symbol,
                    Currency = g.Key.Currency,
                    Total_Quantity = g.Sum(x => x.Quantity),
                    Average_Price = g.Average(x => x.Price),
                    Notional_Base = g.Sum(x => x.Quantity) * g.Average(x => x.Price),
                })
                .ToListAsync();
#pragma warning restore IDE0037 // Use inferred member name

            return result.Select(trade => new TradeTransationResponse
            {
                Account = trade.Account,
                AvaragePrice = trade.Average_Price,
                TotalQuantity = trade.Total_Quantity,
                NotionalBase = (double)trade.Notional_Base,
                Symbol = trade.Symbol,
                BaseCurrency = trade.Currency
            });
        }
    }
}
