namespace Backend.Core.Repositories;

using Backend.Core.Models;

public interface IArtistsRepository
{
    Task<IEnumerable<Artist>> GetAll();
    Task<Tuple<Artist, int>> GetByIdAsync(int id, int skip, int take, string? search,
        int? minimumPrice, int? maximumPrice, bool? isSortAscending);
    Task Translate();
    Task<Tuple<IEnumerable<Artist>, int>> GetAsync(int skip, int take, int takeProducts, string? search, int? minimumPrice, int? maximumPrice, bool? isSortAscending);
    Task AddAsync(Artist artist);
    Task RemoveAsync(int id);
    Task UpdateAsync(int id, Artist newArtist);
}