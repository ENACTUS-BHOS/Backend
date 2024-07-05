namespace Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Backend.Core.Models;
using Microsoft.EntityFrameworkCore;

public class MirasDbContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Team> Teams { get; set; } 
    public DbSet<Tutorial> Tutorials { get; set; } 
    public DbSet<Exhibition> Exhibitions { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    public MirasDbContext(DbContextOptions<MirasDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var stringListConverter = new ValueConverter<List<string>, string>(
                v => string.Join(";", v),
                v => v.Split(new[] { ';' }, StringSplitOptions.None).ToList());

            modelBuilder.Entity<Exhibition>()
                .Property(e => e.ImageUrls)
                .HasConversion(stringListConverter);
        }
}