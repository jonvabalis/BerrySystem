using BerrySystem.Core.Queries;
using FluentValidation;

namespace BerrySystem.Core.Validation.Statistics;

public class GetCompareByYearStatisticsQueryValidator : AbstractValidator<GetCompareByYearStatisticsQuery>
{
    public GetCompareByYearStatisticsQueryValidator()
    {
        RuleFor(x => x.BerryTypeId).NotEmpty().WithMessage("No berry type id provided");
        RuleFor(x => x.Years).NotEmpty().WithMessage("No years provided");
        RuleFor(x => x.StartMonth).NotEmpty().WithMessage("No interval start provided");
        RuleFor(x => x.EndMonth).NotEmpty().WithMessage("No interval end provided");
        RuleFor(x => x.StartMonth).InclusiveBetween(1, 12).WithMessage("Start month must be between 1 and 12.");
        RuleFor(x => x.EndMonth).InclusiveBetween(1, 12).WithMessage("End month must be between 1 and 12.");
        RuleFor(x => x).Must(x => x.StartMonth <= x.EndMonth)
            .WithMessage("Start month must not be later than end month.");
    }
}