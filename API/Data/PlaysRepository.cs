
namespace API;

public class PlaysRepository : IPlaysRepository
{
    private readonly List<Play> plays =
    [
        new Play
        {
            Id = 1,
            Name = "Hamlet",
            Type = "Tragedy",
            TicketPrice = 120M
        },
        new Play
        {
            Id = 2,
            Name = "As You Like It",
            Type = "Comedy",
            TicketPrice = 100M
        },
        new Play
        {
            Id = 3,
            Name = "Othello",
            Type = "Tragedy",
            TicketPrice = 90M
        }
    ];

    public Task<IEnumerable<Play>> GetAllAsync()
    {
        // Simulate a database connection error
        throw new InvalidOperationException("The database connection is closed!");
    }

    public async Task<Play?> GetAsync(int id)
    {
        // 0 or negative ids are not allowed
        if (id < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "The play Id must be greater than 0!");
        }

        return await Task.FromResult(plays.Find(p => p.Id == id));
    }
}
