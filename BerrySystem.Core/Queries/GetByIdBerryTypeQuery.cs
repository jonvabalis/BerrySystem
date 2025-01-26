using BerrySystem.Domain.Entities;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetByIdBerryTypeQuery : IRequest<BerryType>
{
    public Guid BerryTypeId { get; set; }
}