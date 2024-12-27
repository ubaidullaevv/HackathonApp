namespace Domain.DTOs;

public class AddParticipantDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int TeamId { get; set; }
    public string? Role { get; set; }
    public DateTime JoinDate { get; set; } 
}
