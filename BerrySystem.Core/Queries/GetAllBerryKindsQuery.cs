using BerrySystem.Domain.Entities;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetAllBerryKindsQuery : IRequest<List<BerryKind>>
{
    public Guid BerryTypeId { get; set; }
}