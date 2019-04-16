using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReKreator.DAL.EF.EntitiesConfiguration;
using ReKreator.Domain;

namespace ReKreator.DAL
{
    public class EventContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<EventHolding> EventHoldings { get; set; }
        public DbSet<EventHolding_User> EventHoldings_Users { get; set; }
        public DbSet<EventPlace> EventPlaces { get; set; }
        public DbSet<UserMailing> UserMailings { get; set; }

        public EventContext(DbContextOptions<EventContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserMailingEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EventEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EventHolding_UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EventPlaceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EventHoldingEntityConfiguration());
        }
    }
}
