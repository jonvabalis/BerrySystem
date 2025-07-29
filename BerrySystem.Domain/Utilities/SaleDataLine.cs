using System.Text.Json.Serialization;
using BerrySystem.Domain.Types;

namespace BerrySystem.Domain.Utilities;

public class SaleDataLine
{
    public Guid SaleId { get; set; }
    public double Kilograms { get; set; }
    public double PricePerKilo { get; set; }
    public double TotalPrice { get; set; }
    public Guid EmployeeId { get; set; }
    public SaleType SaleType { get; set; }
    public Guid? BerryKindId { get; set; }
    [JsonIgnore]
    public TimeOnly EventTimeRaw { get; init; }

    public string EventTime => EventTimeRaw.ToString("HH:mm");

}