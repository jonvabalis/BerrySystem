namespace BerrySystem.Domain.Utilities;

public class BulkHarvest
{
    public double Kilograms { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid BerryTypeId { get; set; }
    public Guid? BerryKindId { get; set; }
    public DateTime EventTime { get; set; }
}