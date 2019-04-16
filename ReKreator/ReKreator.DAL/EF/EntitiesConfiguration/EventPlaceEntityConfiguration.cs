using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReKreator.DAL.Constants;
using ReKreator.Domain;

namespace ReKreator.DAL.EF.EntitiesConfiguration
{
    public class EventPlaceEntityConfiguration : IEntityTypeConfiguration<EventPlace>
    {
        public void Configure(EntityTypeBuilder<EventPlace> builder)
        {
            builder.ToTable("EventPlace");

            builder.HasIndex(e => e.Title).IsUnique(true);

            builder.Property(e => e.Title).HasMaxLength(EventPlaceEntityConstants.TitleMaxLength).IsRequired(true);

            builder.HasMany(p => p.EventsHoldings).WithOne(eh => eh.EventPlace).IsRequired(true);
        }
    }
}
