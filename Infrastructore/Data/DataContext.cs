using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructore.Data;

public class DataContext(DbContextOptions<DataContext> options): DbContext(options)
{
  public DbSet<Hackathons> Hackathons { get; set; }
  public DbSet<Teams> Teams { get; set; }
  public DbSet<Participants> Participants { get; set; }
}
