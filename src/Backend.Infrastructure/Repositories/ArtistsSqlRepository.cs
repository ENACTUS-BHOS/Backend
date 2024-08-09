namespace Backend.Infrastructure.Repositories;

using System.Linq;
using Backend.Core.Models;
using Backend.Core.Repositories;
using Backend.Infrastructure.Data;
using Backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

public class ArtistsSqlRepository(MirasDbContext dbContext) : IArtistsRepository
{
    public async Task<IEnumerable<Artist>> GetAll()
    {
        var artists = await dbContext.Artists
            .AsNoTracking()
            .Where(artist => artist.IsActive == true)
            .Where(artist => !string.IsNullOrWhiteSpace(artist.ImageUrl))
            .OrderBy(a => a.Id)
            .ToListAsync();

        return artists
            .DistinctBy(a => a.Id);
    }

    public async Task<Tuple<Artist, int>> GetByIdAsync(int id, int skip, int take, string? search,
        int? minimumPrice, int? maximumPrice, bool? isSortAscending)
    {
        var artist = await dbContext.Artists
            .AsNoTracking()
            .Where(a => a.Id == id)
            .Include(artist => artist.Products
                .Where(p => p.Name.ToLower().Contains((search ?? p.Name).ToLower()) ||
                            (search ?? p.Name).ToLower().Contains(p.Name.ToLower()))
                .Where(p => p.Price >= (minimumPrice ?? 0))
                .Where(p => p.Price <= (maximumPrice ?? int.MaxValue))
                .OrderBy(p => (isSortAscending != null ? (isSortAscending == true ? p.Price : -p.Price) : p.Id))
                .Skip(skip)
                .Take(take))
            .AsSplitQuery()
            .FirstOrDefaultAsync();

        var productsCount = await dbContext.Artists.Where(a => a.Id == id).Select(a => a.Products
            .Where(p => p.Name.ToLower().Contains((search ?? p.Name).ToLower()) ||
                        (search ?? p.Name).ToLower().Contains(p.Name.ToLower()))
            .Where(p => p.Price >= (minimumPrice ?? 0)).Count(p => p.Price <= (maximumPrice ?? int.MaxValue))).FirstOrDefaultAsync();

        return new Tuple<Artist, int>(artist, productsCount);
    }

    public async Task<Tuple<IEnumerable<Artist>, int>> GetAsync(int skip, int take, int takeProducts, string? search,
        int? minimumPrice, int? maximumPrice, bool? isSortAscending)
    {
        var artistsByName = await dbContext.Artists
            .AsNoTracking()
            .Where(artist => artist.IsActive == true)
            .Where(a => a.Name.ToLower().Contains((search ?? a.Name).ToLower()) ||
                        (search ?? a.Name).ToLower().Contains(a.Name.ToLower()))
            .Include(a => a.Products
                .Where(p => p.Price >= (minimumPrice ?? 0))
                .Where(p => p.Price <= (maximumPrice ?? int.MaxValue))
                .OrderBy(p => (isSortAscending != null ? (isSortAscending == true ? p.Price : -p.Price) : p.Id))
                .Take(takeProducts))
            .AsSplitQuery()
            .ToListAsync();

        var artistsByProducts = await dbContext.Artists
            .AsNoTracking()
            .Where(artist => artist.IsActive == true)
            .Include(artist => artist.Products
                .Where(p => p.Name.ToLower().Contains((search ?? p.Name).ToLower()) ||
                            (search ?? p.Name).ToLower().Contains(p.Name.ToLower()))
                .Where(p => p.Price >= (minimumPrice ?? 0))
                .Where(p => p.Price <= (maximumPrice ?? int.MaxValue))
                .OrderBy(p => (isSortAscending != null ? (isSortAscending == true ? p.Price : -p.Price) : p.Id))
                .Take(takeProducts))
            .AsSplitQuery()
            .ToListAsync();

        var artists = artistsByName
            .Union(artistsByProducts
                .Where(artist => artist.Products.Count > 0))
            .OrderBy(a => a.Id)
            .DistinctBy(a => a.Id);

        return new Tuple<IEnumerable<Artist>, int>
        (
            artists
                .Skip(skip)
                .Take(take)
            ,
            artists
                .Count()
        );
    }

    public async Task AddAsync(Artist artist)
    {
        await dbContext.Artists.AddAsync(artist);

        await dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id)
    {
        var artist = await dbContext.Artists.FirstOrDefaultAsync(artist => artist.Id == id);

        artist!.IsActive = false;

        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Artist newArtist)
    {
        var artist = await dbContext.Artists.FirstOrDefaultAsync(artist => artist.Id == id);

        artist!.Name = newArtist.Name;

        artist!.Description = newArtist.Description;

        artist!.Major = newArtist.Major;

        artist!.PhoneNumber = newArtist.PhoneNumber;

        artist!.InstagramUrl = newArtist.InstagramUrl;

        artist!.FacebookUrl = newArtist.FacebookUrl;

        await dbContext.SaveChangesAsync();
    }

    public async Task Translate()
    {
        var artists = await GetAll();

        foreach (var artist in artists)
        {
            artist.NameEn = await TranslateService.Translate(artist.Name);

            artist.MajorEn = await TranslateService.Translate(artist.Major);

            artist.DescriptionEn = await TranslateService.Translate(artist.Description);

            await dbContext.SaveChangesAsync();
        }
    }
}