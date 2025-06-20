﻿using BerrySystem.Core.Commands;
using BerrySystem.Infrastructure;
using MediatR;

namespace BerrySystem.Core.Handlers.Harvest;

public class CreateHarvestCommandHandler(BerrySystemDbContext berrySystemDbContext)
    : IRequestHandler<CreateHarvestCommand, Guid>
{
    public async Task<Guid> Handle(CreateHarvestCommand request, CancellationToken cancellationToken)
    {
        var berryType = await berrySystemDbContext.BerryTypes
            .FindAsync([request.BerryTypeId], cancellationToken);

        if (berryType == null)
            throw new Exception($"BerryType with ID {request.BerryTypeId} not found.");

        Domain.Entities.BerryKind? berryKind = null;
        if (request.BerryKindId.HasValue)
        {
            berryKind = await berrySystemDbContext.BerryKinds
                .FindAsync([request.BerryKindId.Value], cancellationToken);

            if (berryKind == null)
                throw new Exception($"BerryKind with ID {request.BerryKindId} was not found.");
        }

        //TODO: Validation
        // if (berryKind != null && berryKind.BerryType.Id != berryType.Id)
        // {
        //     throw new Exception("The specified BerryKind does not belong to the given BerryType.");
        // }

        var harvest = new Domain.Entities.Harvest
        {
            Kilograms = request.Kilograms,
            EmployeeId = request.EmployeeId,
            EventTime = TimeZoneInfo.ConvertTimeFromUtc(
                DateTime.UtcNow,
                TimeZoneInfo.FindSystemTimeZoneById("Europe/Vilnius")
            ),
            BerryType = berryType,
            BerryKind = berryKind,
        };

        await berrySystemDbContext.Harvests.AddAsync(harvest, cancellationToken);
        await berrySystemDbContext.SaveChangesAsync(cancellationToken);

        return harvest.Id;
    }
}