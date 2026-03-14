using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Trader.Backend.Api.Configurations
{
    public class TradeTransactionConfigurations : IEntityTypeConfiguration<TradeTransaction>
    {
        public void Configure(EntityTypeBuilder<TradeTransaction> builder)
        {
            builder.ToTable("TradeTransactions");

            builder.HasKey(t => t.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.TradeAccountId).IsRequired();
            builder.Property(x => x.Currency).IsRequired().HasMaxLength(5);
            builder.Property(x => x.Symbol).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Side).IsRequired().HasMaxLength(50);
            builder.Property(x=>x.Account).HasMaxLength(50);

            builder.HasData(
                new TradeTransaction
                {
                    Id = 1,
                    Currency = "USD",
                    TradeAccountId = 1,
                    Price = 100M,
                    Quantity = 100,
                    Side = "Side1",
                    Symbol = "Symbol1",
                    TradeRateId = 1,
                    TradeTime = DateTime.Now,
                    Account = "ACC-123"
                },
                 new TradeTransaction
                 {
                     Id = 2,
                     Currency = "USD",
                     TradeAccountId = 1,
                     Price = 250M,
                     Quantity = 50,
                     Side = "Side2",
                     Symbol = "Symbol2",
                     TradeRateId = 1,
                     TradeTime = DateTime.Now,
                     Account = "ACC-123"
                 });
        }
    }
}
