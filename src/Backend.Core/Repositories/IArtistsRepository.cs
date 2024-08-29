namespace Backend.Core.Repositories;

using Backend.Core.Models;

public interface IArtistsRepository
{
    Task<IEnumerable<Artist>> GetAll();

    Task<int> GetCountAsync(int id, string? search,
        int? minimumPrice, int? maximumPrice);
    Task<Artist> GetByIdAsync(int id, int skip, int take, string? search,
        int? minimumPrice, int? maximumPrice, bool? isSortAscending);
    Task Translate();
    Task<ProductsCount> GetAsync(int skip, int take, int takeProducts, string? search, int? minimumPrice, int? maximumPrice, bool? isSortAscending, int? guid);
    Task AddAsync(Artist artist);
    Task RemoveAsync(int id);
    Task UpdateAsync(int id, Artist newArtist);
}