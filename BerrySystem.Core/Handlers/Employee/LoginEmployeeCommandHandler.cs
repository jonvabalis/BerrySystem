using BerrySystem.Core.Commands;
using BerrySystem.Core.Services.Interfaces;
using BerrySystem.Domain.Dtos;
using BerrySystem.Infrastructure;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Employee;

public class LoginEmployeeCommandHandler(
    BerrySystemDbContext berrySystemDbContext,
    IPasswordHasherService passwordHasher,
    IJwtService jwtService) : IRequestHandler<LoginEmployeeCommand, AccessTokenUserIdDto?>
{
    public async Task<AccessTokenUserIdDto?> Handle(LoginEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await berrySystemDbContext.Employees
            .Include(e => e.Roles)
            .FirstOrDefaultAsync(e => e.Email == request.LoginCredential || e.Username == request.LoginCredential, cancellationToken);

        if (employee is null || passwordHasher.Verify(request.Password, employee.Password!) is false)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ("Login","Incorrect email or password")
            });
        }

        if (!employee.IsActive)
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ("Login","Employee is not activated yet")
            });
        }

        var response = new AccessTokenUserIdDto
        {
            EmployeeId = employee.Id,
            AccessToken = jwtService.GenerateJwtToken(employee)
        };

        return response;
    }
}