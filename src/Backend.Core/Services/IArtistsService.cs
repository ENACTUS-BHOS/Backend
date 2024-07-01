namespace Backend.Core.Services;

using Backend.Core.Models;

public interface IArtistsService
{
    IEnumerable<Artist> GetAll();
    Task AddAsync(Artist? artist);
    Task RemoveAsync(int? id);
    Task UpdateAsync(int? id, Artist? newArtist);
}