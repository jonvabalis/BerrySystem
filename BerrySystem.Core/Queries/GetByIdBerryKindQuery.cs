using BerrySystem.Domain.Entities;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetByIdBerryKindQuery : IRequest<BerryKind>
{
    public Guid BerryKindId { get; set; }
}