using Domain.DTOs;
using Domain.Entities;
using Infrastructore.Responses;

namespace Infrastructore.Interfaces;

public interface IHackathonService
{
    Task<Response<List<Hackathons>>> GetAllHackathons();
    Task<Response<Hackathons>> GetHackathonById(int id);
    Task<Response<string>> CreateHackathon(AddHackathonDto hackathon);
    Task<Response<string>> UpdateHackathon(Hackathons hackathon);
    Task<Response<string>> DeleteHackathon(int id);
}
