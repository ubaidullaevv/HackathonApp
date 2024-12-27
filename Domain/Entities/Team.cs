namespace Domain.Entities;

public class Teams
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int HackathonId { get; set; }
    public DateTime CreatedDate { get; set; }
    public Hackathons Hackathon { get; set; }
    public List<Participants> Participant { get; set; }
}
