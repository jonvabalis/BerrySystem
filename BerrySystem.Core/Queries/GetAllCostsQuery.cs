using BerrySystem.Domain.Entities;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetAllCostsQuery : IRequest<List<Cost>>
{

}