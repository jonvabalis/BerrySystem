using BerrySystem.Core.Commands;
using BerrySystem.Core.Services.Interfaces;
using BerrySystem.Infrastructure;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Employee;

public class CreateEmployeeCommandHandler(BerrySystemDbContext berrySystemDbContext, IPasswordHasherService passwordHasher)
: IRequestHandler<CreateEmployeeCommand, Guid>
{
    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        if (await berrySystemDbContext.Employees.AnyAsync(e => 
                (!string.IsNullOrEmpty(request.Username) || !string.IsNullOrEmpty(request.Email)) &&
                (e.Email == request.Email || e.Username == request.Username),
                cancellationToken))
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ("Login","Email or username already exists")
            });
        }

        var isAccountRegistration = request.Password is not null &&
                                    (request.Email ?? request.Username) is not null;
        string? hashedPassword = null;
        if (isAccountRegistration)
        {
            hashedPassword = passwordHasher.Hash(request.Password!);
        }

        var employee = new Domain.Entities.Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Birthday = request.Birthday,
            Password = hashedPassword,
            Username = request.Username,
        };

        await berrySystemDbContext.Employees.AddAsync(employee, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}