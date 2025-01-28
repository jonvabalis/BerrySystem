using BerrySystem.Domain.Entities;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetByNameBerryTypeQuery : IRequest<BerryType>
{
    public string BerryType { get; set; } = string.Empty;
}