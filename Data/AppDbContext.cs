using metafar_challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace metafar_challenge.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
    }
}
