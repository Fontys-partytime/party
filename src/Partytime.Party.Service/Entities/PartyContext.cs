using Microsoft.EntityFrameworkCore;

namespace Partytime.Party.Service.Entities
{
    public class PartyContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<Party>().HasMany(joined => joined.Joined).WithOne(party => party.Party);
        }

        public PartyContext(DbContextOptions<PartyContext> options)
        : base(options)
        {
        }

        public DbSet<Party> Parties { get; set; } = null!;
        public DbSet<Joined> Joined { get; set; } = null!;
    }
}
