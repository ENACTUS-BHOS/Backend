public class ExhibitionService : IExhibitionService
{
    private readonly IExhibitionRepository _repository;

    public ExhibitionService(IExhibitionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Exhibition>> GetExhibitionsAsync()
    {
        return await _repository.GetExhibitionsAsync();
    }

    public async Task<Exhibition> GetExhibitionByIdAsync(int id)
    {
        return await _repository.GetExhibitionByIdAsync(id);
    }

    public async Task<IEnumerable<Exhibition>> SearchExhibitionsAsync(string query)
    {
        return await _repository.SearchExhibitionsAsync(query);
    }

    public async Task AddExhibitionAsync(Exhibition exhibition)
    {
        await _repository.AddExhibitionAsync(exhibition);
        await _repository.SaveChangesAsync();
    }
}
