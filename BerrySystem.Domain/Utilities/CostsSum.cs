namespace BerrySystem.Domain.Utilities;

public class CostsSum(double cost)
{
    public double Costs { get; set; } = cost;

    public CostsSum() : this(0)
    {
    }
}