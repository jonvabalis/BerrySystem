namespace BerrySystem.Domain.Entities;

public class BerryKind : Entity
{
    public string Kind { get; set; } = string.Empty;
    public Guid BerryTypeId { get; set; }
    public BerryType BerryType { get; private set; }
}