using MediatR;

namespace BerrySystem.Core.Commands;

public class CreateHarvestCommand : IRequest<Guid>
{
    public double Kilograms { get; set; }
    public Guid EmployeeId { get; set; }
}