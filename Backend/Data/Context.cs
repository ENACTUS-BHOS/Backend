using Microsoft.EntityFrameworkCore;

public class ExhibitionContext : DbContext
{
    public DbSet<Exhibition> Exhibitions { get; set; }

    public ExhibitionContext(DbContextOptions<ExhibitionContext> options)
        : base(options)
    {
    }
}