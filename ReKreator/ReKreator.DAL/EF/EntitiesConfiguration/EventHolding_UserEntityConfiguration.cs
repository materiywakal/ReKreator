using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReKreator.Domain;
using ReKreator.Domain.Enums;

namespace ReKreator.DAL.EF.EntitiesConfiguration
{
    public class EventHolding_UserEntityConfiguration : IEntityTypeConfiguration<EventHolding_User>
    {
        public void Configure(EntityTypeBuilder<EventHolding_User> builder)
        {
            builder.ToTable("EventHolding_User");

            builder.HasKey(e => new {e.UserId, e.EventHoldingId});

            builder.Property(e => e.NotificationPeriodsBeforeEvent).IsRequired(true).HasDefaultValue(NotificationPeriod.None); ;

            // EventHolding-User : many-to-many
            builder.HasOne(ehu => ehu.EventHolding).WithMany(eh => eh.EventHoldings_Users).HasForeignKey(ehu => ehu.EventHoldingId);
            builder.HasOne(ehu => ehu.User).WithMany(eh => eh.EventHoldings_Users).HasForeignKey(ehu => ehu.UserId);
        }
    }
}
