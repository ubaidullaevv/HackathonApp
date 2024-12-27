using System.Net;
using Domain.DTOs;
using Domain.Entities;
using Infrastructore.Data;
using Infrastructore.Interfaces;
using Infrastructore.Responses;
using Microsoft.EntityFrameworkCore;

namespace Infrastructore.Services;

public class ParticipantService(DataContext context) : IParticipentService
{
  


    public async Task<Response<string>> CreateParticipant(AddParticipantDto request)
    {
        var Participant= new Participants(){
            Name = request.Name,
             Email= request.Email,
             Role= request.Role,
             TeamId= request.TeamId
        };
        await context.Participants.AddAsync(Participant);
        var res=await context.SaveChangesAsync();
        return res==0
        ? new Response<string>(HttpStatusCode.InternalServerError, "Team not created")
        : new Response<string>("Product created successfully");
    }

    public async Task<Response<string>> DeleteParticipant(int id)
    {
        var exist=await context.Participants.FirstOrDefaultAsync(x=>x.Id==id);
        if(exist==null)
            return new Response<string>(HttpStatusCode.NotFound, "Participant not found");
            context.Participants.Remove(exist);
            var res= await context.SaveChangesAsync();
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Participant not deleted")
            : new Response<string>("Participant deleted successfully");
    }

    public async Task<Response<List<Participants>>> GetAllParticipants()
    {
       var Participants=await context.Participants.Include(t=>t.Team)
       .ToListAsync();

       var teamDto=Participants.Select(c=> new GetParticipantDto()
       {
       Id=c.Id,
       Name=c.Name,
       Email=c.Email,
       Role = c.Role,
       TeamId=c.TeamId
       Team = c.Team.Select(t=> new GetTeamDto()
       {
       Id=t.Id,
       Name=t.Name,
       ParticipantId =t.ParticipantId, 
       CreatedDate=t.CreatedDate
        }).ToList();
       }).ToList();

       return new Response<List<Participants>>(Participants);
    }

    public async Task<Response<Participants>> GetParticipantById(int id)
    {
       
        var exist=await context.Participants.FirstOrDefaultAsync(x=>x.Id==id);
        return exist == null
            ? new Response<Participants>(HttpStatusCode.NotFound, "Participant not found")
            : new Response<Participants>(exist);
    }

    public async Task<Response<string>> UpdateParticipant(Participants participant)
    {
        var exist=await context.Participants.FirstOrDefaultAsync(x=>x.Id==participant.Id);
        if(exist==null)
            return new Response<string>(HttpStatusCode.NotFound, "Participant not found");
            exist.Name=participant.Name;
            exist.Email=participant.Email;
            exist.Role=participant.Role;
            exist.TeamId=participant.TeamId;
            var res= await context.SaveChangesAsync();
            return res==0
            ? new Response<string>(HttpStatusCode.InternalServerError, "Participant not updated")
             : new Response<string>("Participant updated successfully");
    }
}
