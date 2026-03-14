using Microsoft.OpenApi.Models;
using Trader.Backend.Api.Endpoints;
using Trader.Backend.Api.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Trader Minimal API",
        Version = "v1",
        Description = "Trader backend service for trading reports"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("api/v1")
    .WithTags("Trade endpoints")
    .MapTradeEndpoint();

app.Run();