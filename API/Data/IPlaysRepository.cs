namespace API;

public interface IPlaysRepository
{
    public Task<IEnumerable<Play>> GetAllAsync();
    public Task<Play?> GetAsync(int id);
}
