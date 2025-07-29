using BerrySystem.Core.Commands;
using FluentValidation;

namespace BerrySystem.Core.Validation.Employee;

public class LoginEmployeeCommandValidator : AbstractValidator<LoginEmployeeCommand>
{
    public LoginEmployeeCommandValidator()
    {
        RuleFor(x => x).NotEmpty().WithMessage("Enter a valid email or username");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Enter a password");
    }
}
