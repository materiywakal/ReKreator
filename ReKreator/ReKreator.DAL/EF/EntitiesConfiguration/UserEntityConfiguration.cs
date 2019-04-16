using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReKreator.DAL.Constants;
using ReKreator.Domain;

namespace ReKreator.DAL.EF.EntitiesConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.UserName).IsUnique(true);
            builder.HasIndex(e => e.NormalizedUserName).IsUnique(true);
            builder.HasIndex(e => e.Email).IsUnique(true);
            builder.HasIndex(e => e.NormalizedEmail).IsUnique(true);
            builder.HasIndex(e => e.PasswordHash).IsUnique(false);
            
            builder.Property(e => e.Id).HasColumnName("UserId");
            builder.Property(e => e.RegistrationDate).HasColumnName("RegisterOn");
            builder.Property(e => e.UserName).HasMaxLength(UserEntityConstants.UsernameMaxLength).IsRequired(true);
            builder.Property(e => e.Email).HasMaxLength(UserEntityConstants.EmailMaxLength).IsRequired(true);
            builder.Property(e => e.NormalizedUserName).HasMaxLength(UserEntityConstants.UsernameMaxLength).IsRequired(true);
            builder.Property(e => e.NormalizedEmail).HasMaxLength(UserEntityConstants.EmailMaxLength).IsRequired(true);
            builder.Property(e => e.PasswordHash).HasMaxLength(UserEntityConstants.PasswordHashMaxLength).IsRequired(true);
            builder.Property(e => e.FirstName).HasMaxLength(UserEntityConstants.FirstNameMaxLength).IsRequired(false);
            builder.Property(e => e.LastName).HasMaxLength(UserEntityConstants.LastNameMaxLength).IsRequired(false);
            builder.Property(e => e.EmailConfirmed).IsRequired(true);
            builder.Property(e => e.SecurityStamp).HasMaxLength(255).IsRequired(false);
            builder.Property(e => e.ConcurrencyStamp).HasMaxLength(255).IsRequired(false);
            builder
                .Ignore(e => e.PhoneNumber)
                .Ignore(e => e.PhoneNumberConfirmed);

            builder.HasOne(u => u.UserMailing).WithOne().HasForeignKey<UserMailing>(e => e.UserId).IsRequired(true);
        }
    }
}
