using BerrySystem.Core.Commands;
using BerrySystem.Core.Validation;
using BerrySystem.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BerrySystem.Core.Handlers.BerryKind;

public class CreateBerryKindCommandHandler(BerrySystemDbContext berrySystemDbContext)
: IRequestHandler<CreateBerryKindCommand, Guid>
{
    public async Task<Guid> Handle(CreateBerryKindCommand request, CancellationToken cancellationToken)
    {
        var berryTypeExists = await berrySystemDbContext.BerryTypes.AnyAsync(bt => bt.Id == request.BerryTypeId, cancellationToken);
        if (!berryTypeExists)
        {
            throw new NotFoundException("BerryType", request.BerryTypeId);
        }
        
        var berryKind = new Domain.Entities.BerryKind
        {
            Kind = request.Kind,
            BerryTypeId = request.BerryTypeId,
        };

        await berrySystemDbContext.BerryKinds.AddAsync(berryKind, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return berryKind.Id;
    }
}