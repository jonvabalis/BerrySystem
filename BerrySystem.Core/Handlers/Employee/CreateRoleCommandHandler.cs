using BerrySystem.Core.Commands;
using BerrySystem.Domain.Entities;
using BerrySystem.Infrastructure;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.Employee;

public class CreateRoleCommandHandler(BerrySystemDbContext berrySystemDbContext) : IRequestHandler<CreateRoleCommand, Guid>
{
    public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (await berrySystemDbContext.Roles.AnyAsync(r => r.Name == request.Name, cancellationToken))
        {
            throw new ValidationException(new List<ValidationFailure>
            {
                new ("Role","Role already exists")
            });
        }

        var role = new Role
        {
            Name = request.Name,
        };

        await berrySystemDbContext.Roles.AddAsync(role, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);
        
        return role.Id;
    }
}