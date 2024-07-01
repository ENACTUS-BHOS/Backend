namespace Backend.Infrastructure.Data;

using Backend.Core.Models;
using Microsoft.EntityFrameworkCore;

public class MirasDbContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }

    public MirasDbContext(DbContextOptions<MirasDbContext> options) : base(options) { }
}