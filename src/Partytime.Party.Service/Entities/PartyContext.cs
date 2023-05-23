using Microsoft.EntityFrameworkCore;

namespace Partytime.Party.Service.Entities
{
    public class PartyContext : DbContext
    {
        public PartyContext(DbContextOptions<PartyContext> options)
        : base(options)
        {
        }

        public DbSet<Party> Parties { get; set; } = null!;
    }
}
