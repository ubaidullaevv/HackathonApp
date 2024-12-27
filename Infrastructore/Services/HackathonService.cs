using System.Net;
using Domain.DTOs;
using Domain.Entities;
using Infrastructore.Data;
using Infrastructore.Interfaces;
using Infrastructore.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructore.Services;

public class HackathonService(DataContext context) : IHackathonService
{
  


    public async Task<Response<string>> CreateHackathon(AddHackathonDto request)
    {
        var hackathon= new Hackathons(){
            Name = request.Name,
            Date = request.Date,
             Theme= request.Theme
        };
        await context.Hackathons.AddAsync(hackathon);
        var res=await context.SaveChangesAsync();
        return res==0
        ? new Response<string>(HttpStatusCode.InternalServerError, "Team not created")
        : new Response<string>("Product created successfully");
    }

    public async Task<Response<string>> DeleteHackathon(int id)
    {
        var exist=await context.Hackathons.FirstOrDefaultAsync(x=>x.Id==id);
        if(exist==null)
            return new Response<string>(HttpStatusCode.NotFound, "Hackathon not found");
            context.Hackathons.Remove(exist);
            var res= await context.SaveChangesAsync();
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Hackathon not deleted")
            : new Response<string>("Hackathon deleted successfully");
    }

    public async Task<Response<List<Hackathons>>> GetAllHackathons()
    {
       var hackathons=await context.Hackathons.Include(t=>t.Teams)
       .ToListAsync();

       var teamDto=hackathons.Select(c=> new GetHackathonsDto()
       {
       Id=c.Id,
       Name=c.Name,
       Date=c.Date,
       Theme = c.Theme,
       Teams = c.Teams.Select(t=> new GetTeamDto()
       {
       Id=t.Id,
       Name=t.Name,
       HackathonId =t.HackathonId, 
       CreatedDate=t.CreatedDate
        }).ToList();
       }).ToList();

       return new Response<List<Hackathons>>(hackathons);
    }

    public async Task<Response<Hackathons>> GetHackathonById(int id)
    {
       
        var exist=await context.Hackathons.FirstOrDefaultAsync(x=>x.Id==id);
        return exist == null
            ? new Response<Hackathons>(HttpStatusCode.NotFound, "Hackathon not found")
            : new Response<Hackathons>(exist);
    }

    public async Task<Response<string>> UpdateHackathon(Hackathons hackathon)
    {
        var exist=await context.Hackathons.FirstOrDefaultAsync(x=>x.Id==hackathon.Id);
        if(exist==null)
            return new Response<string>(HttpStatusCode.NotFound, "Hackathon not found");
            exist.Name=hackathon.Name;
            exist.Date=hackathon.Date;
            exist.Theme=hackathon.Theme;
            var res= await context.SaveChangesAsync();
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Hackathon not updated")
             : new Response<string>("Hackathon updated successfully");
    }
}
