using BerrySystem.Domain.Types;

namespace BerrySystem.Domain.Entities;

public class Sale : Entity
{
    public double Kilograms { get; set; }
    public double PricePerKilo { get; set; }
    public double TotalPrice { get; set; }
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; set; } = null!;
    public SaleType SaleType { get; set; }
    public DateTime EventTime { get; set; }
    public BerryType BerryType { get; set; }
    public BerryKind? BerryKind { get; set; }
}