using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Queries;

public class GetAllRolesQuery : IRequest<List<RoleDto>>;