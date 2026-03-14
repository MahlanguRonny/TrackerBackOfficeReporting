using Tracker.Backend.Domain.Models;
using Trader.Backend.Api.Contracts.Requests;
using Trader.Backend.Api.Services;

namespace Trader.Backend.Api.Endpoints
{
    //define all the trade related endpoints here, so we avoid clutter in the program.cs
    public static class TradeEndpoint
    {
        public static IEndpointRouteBuilder MapTradeEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/TradeTransactions", async (CreateTradeTransactionRequest tradeTransaction, IApiTraderService tradeService) =>
            {
                var result = await tradeService.AddTradeTransactionAsync(tradeTransaction);
                return Results.Created($"TradeTransactions/{result.Id}", result);
            });

            app.MapPost("/TradeTransactionsByDate", async (GetTradeTransactionRequest tradeTransaction, IApiTraderService tradeService) =>
            {
                var result = await tradeService.TradeTransactionsByDate(tradeTransaction);
                return Results.Ok(result);
            });

            return app;
        }
    }
}
