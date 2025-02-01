namespace BerrySystem.Domain.Utilities;

public class CollectionStatisticsLine(double harvestKilograms, double saleKilograms, double saleTotalPrice)
{
    public double HarvestKilograms { get; set; } = harvestKilograms;
    public double SaleKilograms { get; set; } = saleKilograms;
    public double SaleTotalPrice { get; set; } = saleTotalPrice;
}