using BerrySystem.Domain.Dtos;
using MediatR;

namespace BerrySystem.Core.Commands;

public class LoginEmployeeCommand : IRequest<AccessTokenUserIdDto?>
{
    public required string LoginCredential { get; set; }
    public required string Password { get; set; }
}