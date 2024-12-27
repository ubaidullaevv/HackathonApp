namespace Domain.Entities;

public class Hackathons
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string? Theme { get; set; }
    public List<Teams> Teams { get; set; }
}
