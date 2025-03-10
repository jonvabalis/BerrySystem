using BerrySystem.Domain.Types;

namespace BerrySystem.Domain.Utilities;

public class BulkSale
{
    public double Kilograms { get; set; }
    public double PricePerKilo { get; set; }
    public double TotalPrice { get; set; }
    public Guid EmployeeId { get; set; }
    public SaleType SaleType { get; set; }
    public Guid BerryTypeId { get; set; }
    public Guid? BerryKindId { get; set; }
    public DateTime EventTime { get; set; }
}