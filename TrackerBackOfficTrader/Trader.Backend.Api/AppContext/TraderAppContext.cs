using Microsoft.EntityFrameworkCore;

namespace Trader.Backend.Api.AppContext
{
    public class TraderAppContext(DbContextOptions<TraderAppContext> options): DbContext(options)
    {
        private const string DefaultSchema = "traderapi";

        public DbSet<TradeAccount> RateAccounts { get; set; }
        public DbSet<TradeRate> TradeRates { get; set; }
        public DbSet<TradeTransaction> TradeTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema(DefaultSchema);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TraderAppContext).Assembly);
        }
    }
}
