namespace Backend.Infrastructure.Repositories;

using System.Linq;
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

    public async Task<IEnumerable<Artist>> GetAsync(int skip, int take, string? search, int? minimumPrice, int? maximumPrice, bool? isSortAscending)
    {
        var artists = this.dbContext.Artists.AsEnumerable();

        if(!string.IsNullOrWhiteSpace(search))
        {
            artists = artists.Where(a => a.Name.ToLower().Contains(search.ToLower()) || search.ToLower().Contains(a.Name.ToLower()));
        }

        var products = this.dbContext.Products.AsEnumerable();

        if(!string.IsNullOrWhiteSpace(search))
        {
            products = products.Where(p => p.Name.ToLower().Contains(search.ToLower()) || search.ToLower().Contains(p.Name.ToLower()));
        }

        if(minimumPrice != null)
        {
            products = products.Where(p => p.Price > minimumPrice);
        }

        if(maximumPrice != null)
        {
            products = products.Where(p => p.Price < maximumPrice);
        }

        var artistsIds = artists.ToList().Select(p => p.Id).ToList();

        foreach(var product in products.ToList())
        {
            if(!artistsIds.Contains((int)product.ArtistId!))
            {
                var artist = this.dbContext.Artists.ToList().FirstOrDefault(a => a.Id == (int)product.ArtistId!);

                artists = artists.Append(artist)!;

                artistsIds = artists.ToList().Select(p => p.Id).ToList();
            }
        }

        artists = artists.Skip(skip).Take(take);

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
        
        artist!.PhoneNumber = newArtist.PhoneNumber;

        artist!.InstagramUrl = newArtist.InstagramUrl;

        artist!.FacebookUrl = newArtist.FacebookUrl;

        await this.dbContext.SaveChangesAsync();
    }
}