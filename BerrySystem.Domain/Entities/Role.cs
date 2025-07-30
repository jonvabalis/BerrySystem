namespace BerrySystem.Domain.Entities;

public class Role : Entity
{
    public required string Name { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
}