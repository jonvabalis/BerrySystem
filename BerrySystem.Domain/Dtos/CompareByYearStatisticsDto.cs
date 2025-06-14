namespace BerrySystem.Domain.Dtos;

public class CompareByYearStatisticsDto
{
    public required List<Dictionary<string, string>> HarvestKilograms { get; set; }
    public required List<Dictionary<string, string>> SaleKilograms { get; set; }
    public required List<Dictionary<string, string>> SaleRevenue { get; set; }
}