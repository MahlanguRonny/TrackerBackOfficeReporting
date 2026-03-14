namespace Trader.Backend.Api.Exceptions
{
    public class NoTradeFoundException: Exception
    {
        public NoTradeFoundException(): base("No trade record/transaction found") { }
    }
}
