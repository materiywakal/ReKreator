using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReKreator.Domain;

namespace ReKreator.DAL.EF.EntitiesConfiguration
{
    public class EventHoldingEntityConfiguration : IEntityTypeConfiguration<EventHolding>
    {
        public void Configure(EntityTypeBuilder<EventHolding> builder)
        {
            builder.ToTable("EventHolding");

            builder.HasIndex(e => e.Time).IsUnique(false);

            builder.Property(e => e.Time).IsRequired(true);
        }
    }
}
