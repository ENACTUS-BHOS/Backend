namespace Backend.Infrastructure.Repositories;

using Backend.Core.Models;
using Backend.Core.Repositories;
using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ArtistsSqlRepository : IArtistsRepository
{
    private readonly MirasDbContext dbContext;

    public ArtistsSqlRepository(MirasDbContext dbContext) => this.dbContext = dbContext;

    public IEnumerable<Artist> GetAll()
    {
        var artist = this.dbContext.Artists.Where(artist => artist.IsActive == true);

        return artist;
    }

    public async Task<Artist> GetByIdAsync(int id)
    {
        var artist = await this.dbContext.Artists.FirstOrDefaultAsync(a => a.Id == id);

        return artist!;
    }

    public IEnumerable<Artist> Get(int skip, int take)
    {
        var artists = this.dbContext.Artists.Skip(skip).Take(take);

        return artists;
    }

    public async Task AddAsync(Artist artist)
    {
        await this.dbContext.Artists.AddAsync(artist);

        await this.dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var artist = await this.dbContext.Artists.FirstOrDefaultAsync(artist => artist.Id == id);

        artist!.IsActive = false;

        await this.dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Artist newArtist)
    {
        var artist = await this.dbContext.Artists.FirstOrDefaultAsync(artist => artist.Id == id);

        artist!.Name = newArtist.Name;

        artist!.Description = newArtist.Description;

        artist!.Major = newArtist.Major;

        await this.dbContext.SaveChangesAsync();
    }
}