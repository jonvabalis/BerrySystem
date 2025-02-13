namespace BerrySystem.Domain.Utilities;

public class CollectionStatisticsLine(double harvestKilograms = 0, double saleKilograms = 0, double saleTotalPrice = 0)
{
    public double HarvestKilograms { get; set; } = harvestKilograms;
    public double SaleKilograms { get; set; } = saleKilograms;
    public double SaleTotalPrice { get; set; } = saleTotalPrice;

    public CollectionStatisticsLine() : this(0) { }
}