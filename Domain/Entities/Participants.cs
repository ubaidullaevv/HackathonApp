namespace Domain.Entities;

public class Participants
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int TeamId { get; set; }
    public string? Role { get; set; }
    public DateTime JoinDate { get; set; }
    public Teams Team { get; set; }
}
