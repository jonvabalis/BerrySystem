namespace BerrySystem.Domain.Entities;

public class Employee : Entity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime Birthday { get; set; }
    public required string Password { get; set; }
    public bool IsActive { get; set; } = false;
}