using Microsoft.EntityFrameworkCore;
using BerrySystem.Domain.Entities;

namespace BerrySystem.Infrastructure;

public class BerrySystemDbContext : DbContext
{
    public BerrySystemDbContext(DbContextOptions<BerrySystemDbContext> options) : base(options)
    {
        
    }

    public DbSet<Harvest> Harvests { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Sale> Sales { get; set; }
    
    public DbSet<BerryType> BerryTypes { get; set; }
    public DbSet<BerryKind> BerryKinds { get; set; }
    public DbSet<Cost> Costs { get; set; }
}