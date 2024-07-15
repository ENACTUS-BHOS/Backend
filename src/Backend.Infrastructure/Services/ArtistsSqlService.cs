namespace Backend.Infrastructure.Services;

using Backend.Core.Models;
using Backend.Core.Repositories;
using Backend.Core.Services;

public class ArtistsSqlService : IArtistsService
{
    private readonly IArtistsRepository artistsRepository;

    public ArtistsSqlService(IArtistsRepository artistsRepository) => this.artistsRepository = artistsRepository;

    public IEnumerable<Artist> GetAll()
    {
        var artist = this.artistsRepository.GetAll();

        return artist;
    }

    public async Task<Artist> GetByIdAsync(int? id)
    {
        ArgumentNullException.ThrowIfNull(id);

        var artist = await this.artistsRepository.GetByIdAsync((int)id);

        return artist;
    }

    public async Task<IEnumerable<Artist>> GetAsync(int? skip, int? take, string? search, int? minimumPrice, int? maximumPrice, bool? isSortAscending)
    {
        ArgumentNullException.ThrowIfNull(skip);

        ArgumentNullException.ThrowIfNull(take);

        var artists = await this.artistsRepository.GetAsync((int)skip, (int)take, search, minimumPrice, maximumPrice, isSortAscending);

        return artists;
    }

    public async Task AddAsync(Artist? artist)
    {
        ArgumentNullException.ThrowIfNull(artist);

        await this.artistsRepository.AddAsync(artist);
    }

    public async Task RemoveAsync(int? id)
    {
        ArgumentNullException.ThrowIfNull(id);

        await this.artistsRepository.RemoveAsync((int)id);
    }

    public async Task UpdateAsync(int? id, Artist? newArtist)
    {
        ArgumentNullException.ThrowIfNull(id);

        ArgumentNullException.ThrowIfNull(newArtist);

        await this.artistsRepository.UpdateAsync((int)id, newArtist);
    }
}