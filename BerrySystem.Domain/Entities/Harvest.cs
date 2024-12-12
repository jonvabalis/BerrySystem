namespace BerrySystem.Domain.Entities;

public class Harvest : Entity
{
    public double Kilograms { get; set; }
    public Guid EmployeeId { get; set; }
}