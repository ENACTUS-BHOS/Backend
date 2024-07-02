namespace Backend.Infrastructure.Data;

using Backend.Core.Models;
using Microsoft.EntityFrameworkCore;

public class MirasDbContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Team> Teams { get; set; } 
    public DbSet<Tutorial> Tutorials { get; set; } 
    public DbSet<Exhibition> Exhibitions { get; set; }
    public MirasDbContext(DbContextOptions<MirasDbContext> options) : base(options) { }
}