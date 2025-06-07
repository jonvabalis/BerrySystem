using MediatR;

namespace BerrySystem.Core.Commands;

public class UpdateHarvestCommand : IRequest<bool>
{
    public Guid HarvestId { get; set; }
    public Guid EmployeeId { get; set; }
    public double Kilograms { get; set; }
    public Guid? BerryKindId { get; set; }
}