using Microsoft.EntityFrameworkCore;
using Backend.Core.Models;

namespace Backend.Infrastructure.Data
{
    public class ExhibitionContext : DbContext
    {
        public DbSet<Exhibition> Exhibitions { get; set; } // DbSet for Exhibition model

        public ExhibitionContext(DbContextOptions<ExhibitionContext> options)
            : base(options)
        {
        }

        // No need for additional SaveChangesAsync() method here
        // DbContext already provides SaveChangesAsync()
    }
}
