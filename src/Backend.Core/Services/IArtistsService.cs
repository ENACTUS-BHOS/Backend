namespace Backend.Core.Services;

using Backend.Core.Models;

public interface IArtistsService
{
    IEnumerable<Artist> GetAll();
    Task<Artist> GetByIdAsync(int? id);
    Task<IEnumerable<Artist>> GetAsync(int? skip, int? take, string? search, int? minimumPrice, int? maximumPrice, bool? isSortAscending);
    Task AddAsync(Artist? artist);
    Task RemoveAsync(int? id);
    Task UpdateAsync(int? id, Artist? newArtist);
}