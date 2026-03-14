using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tracker.Backend.Domain.Models;

namespace Trader.Backend.Api.Configurations
{
    public class TradeAccountConfiguration : IEntityTypeConfiguration<TradeAccount>
    {
        public void Configure(EntityTypeBuilder<TradeAccount> builder)
        {
            builder.ToTable("TradeAccounts");

            builder.HasKey(t => t.TradeAccountId);

            builder.Property(x => x.TradeAccountId).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);


            builder.HasData(
                new TradeAccount
                {
                    TradeAccountId = 1,
                    Name = "T-001"
                });
        }
    }
}
