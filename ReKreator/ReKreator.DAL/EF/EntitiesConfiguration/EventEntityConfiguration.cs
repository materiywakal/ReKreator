using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReKreator.DAL.Constants;
using ReKreator.Domain;
using ReKreator.Domain.Enums;

namespace ReKreator.DAL.EF.EntitiesConfiguration
{
    public class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Event");

            builder.HasIndex(e => e.Genres).IsUnique(false);
            builder.HasIndex(e => e.Type).IsUnique(false);

            builder.Property(e => e.CreationDate).HasColumnName("CreateOn");
            builder.Property(e => e.Title).HasMaxLength(EventEntityConstants.TitleMaxLength).IsRequired(true);
            builder.Property(e => e.Description).HasMaxLength(EventEntityConstants.DescriptionMaxLength).IsRequired(true);
            builder.Property(e => e.PosterUrl).HasMaxLength(EventEntityConstants.PosterUrlMaxLength).IsRequired(false);
            builder.Property(e => e.SourceUrl).HasMaxLength(EventEntityConstants.SourceUrlMaxLength).IsRequired(true);
            builder.Property(e => e.Genres).IsRequired(true).HasDefaultValue(EventGenre.None).HasColumnType("bigint"); ;
            builder.Property(e => e.Type).IsRequired(true).HasColumnType("tinyint");
            builder.Property(e => e.StartDate).IsRequired(true);
            builder.Property(e => e.ExpiryDate).IsRequired(true);
            builder.Property(e => e.IsRemoved).IsRequired(true);
            builder.Property(e => e.CreationDate).IsRequired(true);

            builder.HasMany(e => e.EventsHoldings).WithOne(eh => eh.Event).IsRequired(true);
        }
    }
}
