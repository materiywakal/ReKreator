using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReKreator.Domain;
using ReKreator.Domain.Enums;

namespace ReKreator.DAL.EF.EntitiesConfiguration
{
    public class UserMailingEntityConfiguration : IEntityTypeConfiguration<UserMailing>
    {
        public void Configure(EntityTypeBuilder<UserMailing> builder)
        {
            builder.ToTable("UserMailing");
            
            builder.Property(e => e.UserMailingId).ValueGeneratedOnAdd();
            builder.Property(e => e.MailingPeriod).IsRequired(true).HasColumnType("tinyint").HasDefaultValue(NoveltyMailingPeriod.None);
            builder.Property(e => e.LasTimeMailed).IsRequired(true).HasDefaultValue(DateTime.MinValue);
        }
    }
}
