using BerrySystem.Domain.Entities;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetAllActiveEmployeesQuery : IRequest<List<Employee>>
{
    public bool IsActive { get; set; }
}