namespace Backend.Core.Repositories;

using Backend.Core.Models;

public interface IArtistsRepository
{
    IEnumerable<Artist> GetAll();
    Task AddAsync(Artist artist);
    Task RemoveAsync(int id);
    Task UpdateAsync(int id, Artist newArtist);
}