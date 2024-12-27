using Domain.Entities;
using Infrastructore.Responses;

namespace Infrastructore.Interfaces;

public interface IParticipentService
{
    Task<Response<List<Teams>>> GetAllTeams();
    Task<Response<Teams>> GetTeamById(int id);
    Task<Response<string>> CreateTeam(Teams Team);
    Task<Response<string>> UpdateTeam(Teams Team);
    Task<Response<string>> DeleteTeam(int id);
}
