using System.Data.Common;
using System.Diagnostics;
using Tracker.Backend.Domain.Models;
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
                                                             createTradeTransaction.TradeTime, createTradeTransaction.Currency, createTradeTransaction.TradeRateId,
                                                             createTradeTransaction.BatchId
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

        public async Task<IEnumerable<TradeCreationResponse>> AddBatchTradeTransaction(List<CreateTradeTransactionRequest> tradeTransactionBatch)
        {
            TradeCreationResponse tradeCreationResponse = new TradeCreationResponse();
            List<TradeTransaction> tradeTransactions = new List<TradeTransaction>();

            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //No parameters passed, id will be auto generated and date will be the date of trade
                //firstly create the batch so we can have the batch id to link all trade transaction to

                var batchTrade = TradeBatch.Create();
                _context.Add(batchTrade);
                await _context.SaveChangesAsync();

                //get the rate details from the wcf and only there's data return we can continue with the processing
                foreach (var transaction in tradeTransactionBatch.ToList())
                {
                    var rateData = await _traderService.GetCurrentRateByCurrencyAsync(transaction.Currency);
                    if (rateData is not null && batchTrade.Id != 0)
                    {
                        var externalAccountDetails = await _context.RateAccounts.FirstAsync(x => x.TradeAccountId == transaction.ExternalAccountId);
                        if (externalAccountDetails is not null)
                        {
                            var baseCurrenyAmount = transaction.Price * rateData.RateAmount;
                            var trade = TradeTransaction.Create(externalAccountDetails.TradeAccountId, transaction.Account, transaction.Symbol,
                                                                    transaction.Side, transaction.Quantity, baseCurrenyAmount,
                                                                     transaction.TradeTime, transaction.Currency,
                                                                     transaction.TradeRateId, transaction.BatchId
                                                                );

                            tradeTransactions.Add(trade);
                        }
                    }
                }

                await _context.AddRangeAsync(tradeTransactions);
                await _context.SaveChangesAsync();

                await dbTransaction.CommitAsync();

                var result = tradeTransactions.Select(trade => new TradeCreationResponse
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
                });

                return result;

            }
            catch (Exception ex)
            {
                await dbTransaction.CommitAsync();
                _logger.LogInformation($"Trade error occured while batch trading: ", ex.Message);

                throw;
            }
        }

        public async Task<IEnumerable<TradeTransationResponse>> BatchTradeTransactionsByBatchId(int batchId)
        {
            var result = await _context.TradeTransactions
                            .Where(tt => tt.BatchId == batchId)
                                .GroupBy(tt => new
                                {
                                    tt.Account,
                                    tt.Symbol,
                                    tt.Currency
                                })
                                .Select(g => new
                                {
                                    Account = g.Key.Account,
                                    Symbol = g.Key.Symbol,
                                    Currency = g.Key.Currency,
                                    Total_Quantity = g.Sum(x => x.Quantity),
                                    Average_Price = g.Average(x => x.Price),
                                    Notional_Base = g.Sum(x => x.Quantity) * g.Average(x => x.Price)
                                }).ToListAsync();

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
