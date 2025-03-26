using Microsoft.EntityFrameworkCore;
using BerrySystem.Domain.Entities;
using Serilog;

namespace BerrySystem.Infrastructure;

public class BerrySystemDbContext(DbContextOptions<BerrySystemDbContext> options) : DbContext(options)
{
    private static readonly ILoggerFactory LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(logBuilder =>
    {
        logBuilder.AddSerilog();
    });

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLoggerFactory(LoggerFactory);
    }

    public DbSet<Harvest> Harvests { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Sale> Sales { get; set; }

    public DbSet<BerryType> BerryTypes { get; set; }
    public DbSet<BerryKind> BerryKinds { get; set; }
    public DbSet<Cost> Costs { get; set; }
}