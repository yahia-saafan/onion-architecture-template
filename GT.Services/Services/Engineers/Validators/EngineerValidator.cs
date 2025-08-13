using FluentValidation;
using GT.Application.Services.Engineers.Dtos;

namespace GT.Application.Services.Engineers.Validators;

public class EngineerValidator : AbstractValidator<EngineerDto>
{
    public EngineerValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty().WithMessage("Id cannot be empty.");

        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(100).WithMessage("Max length for name is 100 characters.");

        RuleFor(e => e.NameAR)
            .NotEmpty().WithMessage("NameAR cannot be empty.")
            .MaximumLength(100).WithMessage("Max length for NameAR is 100 characters.");
    }
}
