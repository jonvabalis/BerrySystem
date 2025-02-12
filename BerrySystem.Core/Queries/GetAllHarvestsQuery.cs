using BerrySystem.Domain.Entities;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetAllHarvestsQuery : IRequest<List<Domain.Entities.Harvest>>
{

}