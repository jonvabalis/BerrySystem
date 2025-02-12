using MediatR;

namespace BerrySystem.Core.Queries;

public class GetAllSalesQuery : IRequest<List<Domain.Entities.Sale>>
{

}