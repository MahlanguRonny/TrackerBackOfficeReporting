using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.ServiceModel;
using Trader.Backend.Api.AppContext;
using Trader.Backend.Api.Exceptions;
using WcfServiceReference;

namespace Trader.Backend.Api.Extentions
{
    //any service needs to register in this class to avoid clutter in program.cs  and also to keep things cleaner
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this IHostApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            if (builder.Configuration == null) throw new ArgumentNullException(nameof(builder.Configuration));

            // Adding the database context
            builder.Services.AddDbContext<TraderAppContext>(configure =>
            {
                configure.UseSqlServer(builder.Configuration.GetConnectionString("TrackerDatabase"));
            });

            // Adding validators from the current assembly
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //register self defined services, extentions and custom handlers(such as exception handlers) with the DI container
            builder.Services.AddScoped<IApiTraderService, ApiTraderService>();

            builder.Services.AddScoped<ITraderService>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var wcfServiceAddress = configuration["ExternalServices:WcfTraderServiceAddress"];

                var client = new TraderServiceClient(TraderServiceClient.EndpointConfiguration.BasicHttpBinding_ITraderService, new EndpointAddress(wcfServiceAddress));

                return client;
            });

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddProblemDetails();

        }
    }
}
