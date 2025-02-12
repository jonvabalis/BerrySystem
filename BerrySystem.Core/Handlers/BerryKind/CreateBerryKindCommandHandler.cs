using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.BerryKind;

public class CreateBerryKindCommandHandler(BerrySystemDbContext berrySystemDbContext)
: IRequestHandler<CreateBerryKindCommand, Guid>
{
    public async Task<Guid> Handle(CreateBerryKindCommand request, CancellationToken cancellationToken)
    {
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