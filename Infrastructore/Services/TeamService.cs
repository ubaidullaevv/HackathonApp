using System.Net;
using Domain.DTOs;
using Domain.Entities;
using Infrastructore.Data;
using Infrastructore.Interfaces;
using Infrastructore.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructore.Services;

public class TeamService(DataContext context) : ITeamService
{
    public async Task<Response<string>> CreateTeam(AddTeamDto request)
    {
        var team= new Teams(){
            Name = request.Name,
             HackathonId= request.HackathonId,
             CreatedDate= request.CreatedDate
        };
        await context.Teams.AddAsync(team);
        var res=await context.SaveChangesAsync();
        return res==0
        ? new Response<string>(HttpStatusCode.InternalServerError, "Team not created")
        : new Response<string>("Product created successfully");
    }

    public async Task<Response<string>> DeleteTeam(int id)
    {
        var exist=await context.Teams.FirstOrDefaultAsync(x=>x.Id==id);
        if(exist==null)
            return new Response<string>(HttpStatusCode.NotFound, "Team not found");
            context.Teams.Remove(exist);
            var res= await context.SaveChangesAsync();
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Team not deleted")
            : new Response<string>("Team deleted successfully");
    }

    public async Task<Response<List<Teams>>> GetAllTeams()
    {
       var Teams=await context.Teams.Include(t=>t.Participant)
       .ToListAsync();

       var teamDto=Teams.Select(c=> new GetTeamDto()
       {
       Id=c.Id,
       Name=c.Name,
       Date=c.Date,
       Theme = c.Theme,
       
       }).ToList();

       return new Response<List<Teams>>(Teams);
    }

    public async Task<Response<Teams>> GetTeamById(int id)
    {
       
        var exist=await context.Teams.FirstOrDefaultAsync(x=>x.Id==id);
        return exist == null
            ? new Response<Teams>(HttpStatusCode.NotFound, "Course not found")
            : new Response<Teams>(exist);
    }

    public async Task<Response<string>> UpdateTeam(Teams team)
    {
        var exist=await context.Teams.FirstOrDefaultAsync(x=>x.Id==team.Id);
        if(exist==null)
            return new Response<string>(HttpStatusCode.NotFound, "Team not found");
            exist.Name=team.Name;
            exist.HackathonId=team.HackathonId;
            exist.CreatedDate=team.CreatedDate;
            var res= await context.SaveChangesAsync();
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Team not updated")
             : new Response<string>("Team updated successfully");
    }
}

