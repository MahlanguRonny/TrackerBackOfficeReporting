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
            }).Produces<TradeCreationResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithDescription("Enriches trade transaction from external WCF service and saves it in db");

            app.MapPost("/TradeTransactionsByDate", async (GetTradeTransactionRequest tradeTransaction, IApiTraderService tradeService) =>
            {
                var result = await tradeService.TradeTransactionsByDate(tradeTransaction);
                return Results.Ok(result);
            }).Produces<TradeCreationResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithDescription("Retrieves trade transaction between 2 provided dates"); ;

            return app;
        }
    }
}
