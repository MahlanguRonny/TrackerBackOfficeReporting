using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Backend.Domain.Models;

namespace Trader.Backend.Api.Configurations
{
    public class TradeRateConfiguration : IEntityTypeConfiguration<TradeRate>
    {
        public void Configure(EntityTypeBuilder<TradeRate> builder)
        {
            builder.ToTable("TradeRates");

            builder.HasKey(t => t.Id);
            builder.Property(x => x.FromCurrency).IsRequired().HasMaxLength(6);
            builder.Property(x => x.ToCurrency).IsRequired().HasMaxLength(6);

            builder.HasData(new TradeRate
            {
                Id = 1,
                AsOfDate = new DateTime(2026, 01, 15),
                FromCurrency = "EUR",
                ToCurrency = "USD",
                RateAmount = 1.09M
            });

            builder.HasData(new TradeRate
            {
                Id = 2,
                AsOfDate = new DateTime(2026, 01, 15),
                FromCurrency = "USD",
                ToCurrency = "EUR",
                RateAmount = 0.917M
            });
        }
    }
}
