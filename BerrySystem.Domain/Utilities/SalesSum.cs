namespace BerrySystem.Domain.Utilities;

public class SalesSum(double kilograms, double price)
{
    public double Kilograms { get; set; } = kilograms;
    public double TotalPrice { get; set; } = price;
}