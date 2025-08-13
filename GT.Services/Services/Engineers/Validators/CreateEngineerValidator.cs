using FluentValidation;
using GT.Application.Services.Engineers.Dtos;

namespace GT.Application.Services.Engineers.Validators;

public class CreateEngineerValidator : AbstractValidator<CreateEngineerDto>
{
    public CreateEngineerValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(100).WithMessage("Max length for name is 100 characters.");

        RuleFor(e => e.NameAR)
            .NotEmpty().WithMessage("NameAR cannot be empty.")
            .MaximumLength(100).WithMessage("Max length for NameAR is 100 characters.");
    }
}
