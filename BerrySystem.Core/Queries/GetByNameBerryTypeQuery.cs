using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetByNameBerryTypeQuery : IRequest<GetAllBerryTypeDto>
{
    public string BerryType { get; set; } = string.Empty;
}