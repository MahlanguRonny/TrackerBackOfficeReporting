namespace Trader.Backend.Api.Configurations
{
    public class TradeBatchConfiguration : IEntityTypeConfiguration<TradeBatch>
    {
        public void Configure(EntityTypeBuilder<TradeBatch> builder)
        {
            builder.ToTable("TradeBatches");

            builder.HasKey(t => t.Id);
            builder.Property(x => x.DateCreated);

            builder.HasData(new TradeBatch
            {
                Id = 1,
                DateCreated = new DateTime(2026, 03, 15),
            });
        }
    }
}
