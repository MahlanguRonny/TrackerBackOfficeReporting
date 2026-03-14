using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Trader.Backend.Api.AppContext;
using Trader.Backend.Api.Exceptions;
using Trader.Backend.Api.Services;

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
            builder.Services.AddScoped<ITraderService, TraderService>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddProblemDetails();

        }
    }
}
