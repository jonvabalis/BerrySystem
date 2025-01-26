using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.BerryType;

public class CreateBerryTypeCommandHandler(BerrySystemDbContext berrySystemDbContext)
: IRequestHandler<CreateBerryTypeCommand, Guid>
{
    public async Task<Guid> Handle(CreateBerryTypeCommand request, CancellationToken cancellationToken)
    {
        var berryType = new Domain.Entities.BerryType
        {
            Type = request.Type,
        };
        
        await berrySystemDbContext.BerryTypes.AddAsync(berryType, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);
        
        return berryType.Id;
    }
}