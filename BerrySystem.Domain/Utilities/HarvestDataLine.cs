using System.Text.Json.Serialization;

namespace BerrySystem.Domain.Utilities;

public class HarvestDataLine
{
    public Guid HarvestId { get; set; }
    public double Kilograms { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid? BerryKindId { get; set; }
    [JsonIgnore]
    public TimeOnly EventTimeRaw { get; init; }
    
    public string EventTime => EventTimeRaw.ToString("HH:mm");
    
}