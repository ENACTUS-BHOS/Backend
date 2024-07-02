public interface IExhibitionRepository
{
    Task<IEnumerable<Exhibition>> GetExhibitionsAsync();
    Task<Exhibition> GetExhibitionByIdAsync(int id);
    Task<IEnumerable<Exhibition>> SearchExhibitionsAsync(string query);
    Task AddExhibitionAsync(Exhibition exhibition);
    Task SaveChangesAsync();
}


