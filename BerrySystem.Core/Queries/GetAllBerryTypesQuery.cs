using BerrySystem.Domain.Entities;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetAllBerryTypesQuery : IRequest<List<BerryType>>
{
    
}