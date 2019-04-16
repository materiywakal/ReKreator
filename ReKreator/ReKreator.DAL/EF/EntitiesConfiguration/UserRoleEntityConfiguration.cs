using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReKreator.DAL.EF.EntitiesConfiguration
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<IdentityRole<long>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<long>> builder)
        {
            builder.ToTable("UserRole");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("UserRoleId");
            builder.Property(e => e.Name).HasMaxLength(64).IsRequired(true);
            builder.Property(e => e.NormalizedName).HasMaxLength(64).IsRequired(true);
            builder.Property(e => e.ConcurrencyStamp).HasMaxLength(255).IsRequired(false);
        }
    }
}
