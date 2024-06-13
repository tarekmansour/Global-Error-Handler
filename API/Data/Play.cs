namespace API;

public class Play
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Type { get; set; }
    public decimal TicketPrice { get; set; }
}
