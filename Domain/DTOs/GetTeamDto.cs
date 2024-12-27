namespace Domain.DTOs;

public class GetTeamDto
{
     public int Id { get; set; }
    public string Name { get; set; }
    public int HackathonId { get; set; }
    public DateTime CreatedDate { get; set; } 
}
