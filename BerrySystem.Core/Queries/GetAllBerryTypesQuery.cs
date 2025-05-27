using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetAllBerryTypesQuery : IRequest<List<GetAllBerryTypeDto>>
{

}