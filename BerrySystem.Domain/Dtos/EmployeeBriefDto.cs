namespace BerrySystem.Domain.Dtos;

public class EmployeeBriefDto
{
    public required string Name { get; set; }
    public double HarvestedCount { get; set; }
    public double SoldCount { get; set; }
}