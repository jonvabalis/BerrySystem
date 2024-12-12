using Microsoft.EntityFrameworkCore;
using BerrySystem.Domain.Entities;

namespace BerrySystem.Infrastructure;

public class BerrySystemDbContext : DbContext
{
    public BerrySystemDbContext(DbContextOptions<BerrySystemDbContext> options) : base(options)
    {
        
    }

    public DbSet<Harvest> Harvests { get; set; }
}