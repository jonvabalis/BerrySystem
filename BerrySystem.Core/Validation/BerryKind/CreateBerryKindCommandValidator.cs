using BerrySystem.Core.Commands;
using FluentValidation;

namespace BerrySystem.Core.Validation.BerryKind;

public class CreateBerryKindCommandValidator : AbstractValidator<CreateBerryKindCommand>
{
    public CreateBerryKindCommandValidator()
    {
        RuleFor(x => x.Kind).NotEmpty().WithMessage("Berry kind name is required");
        RuleFor(x => x.BerryTypeId).NotEmpty();
    }
}