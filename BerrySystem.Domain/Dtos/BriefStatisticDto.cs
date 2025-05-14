namespace BerrySystem.Domain.Dtos;

public class BriefStatisticDto
{
    public required Dictionary<Guid, EmployeeBriefDto> Employees { get; set; }
    public double HarvestedCount { get; set; }
    public double SoldCount { get; set; }
    public double SoldSum { get; set; }
}