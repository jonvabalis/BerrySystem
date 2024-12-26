using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateEmployeeCommand : IRequest<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? Birthday { get; set; }
}