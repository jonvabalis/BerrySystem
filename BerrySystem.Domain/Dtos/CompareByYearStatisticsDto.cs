namespace BerrySystem.Domain.Dtos;

public class CompareByYearStatisticsDto
{
    public required List<Dictionary<string, double>> HarvestKilograms { get; set; }
    public required List<Dictionary<string, double>> SaleKilograms { get; set; }
    public required List<Dictionary<string, double>> SaleRevenue { get; set; }
}