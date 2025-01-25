namespace BerrySystem.Domain.Entities;

public class BerryKind : Entity
{
    public string Kind { get; private set; } = string.Empty;
    public Guid BerryTypeId { get; private set; }
    public BerryType BerryType { get; private set; }
}