namespace BerrySystem.Domain.Entities;

public class BerryType : Entity
{
    public string Type { get; set; } = string.Empty;
    public ICollection<BerryKind> BerryKinds { get; set; }
}