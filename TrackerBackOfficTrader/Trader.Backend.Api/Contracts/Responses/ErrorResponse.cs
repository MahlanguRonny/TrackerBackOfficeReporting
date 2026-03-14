namespace Trader.Backend.Api.Contracts.Responses
{
    public record ErrorResponse
    {
        public string Title { get; set; } = default!;
        public int StatusCode { get; set; }
        public string Message { get; set; } = default!;

    }
}
