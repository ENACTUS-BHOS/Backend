namespace Backend.Core.Repositories;

using Backend.Core.Models;

public interface IArtistsRepository
{
    IEnumerable<Artist> GetAll();
    Task<Artist> GetByIdAsync(int id);
    IEnumerable<Artist> Get(int skip, int take);
    Task AddAsync(Artist artist);
    Task RemoveAsync(int id);
    Task UpdateAsync(int id, Artist newArtist);
}