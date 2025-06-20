using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Commands;

public class LoginEmployeeCommand : IRequest<AccessTokenUserIdDto?>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}