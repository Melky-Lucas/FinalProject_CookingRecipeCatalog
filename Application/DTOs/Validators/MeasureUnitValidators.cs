using Core.Interfaces.Repositories;
using FluentValidation;

namespace Application.DTOs.Validators
{
    public class CreateMeasureUnitDTOValidator : AbstractValidator<CreateMeasureUnitDTO>
    {
        public CreateMeasureUnitDTOValidator(IMeasureUnitRepository repo)
        {
            RuleFor(x => x.Name.Trim())
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
                .MustAsync(async (name, cancellation) =>
                {
                    return !await repo.HasNameAsync(name);
                });

            RuleFor(x => x.Abbreviation.Trim())
                .NotEmpty().WithMessage("Abbreviation is required.")
                .MaximumLength(10).WithMessage("Abbreviation cannot exceed 10 characters.")
                .MustAsync(async (abb, cancellation) =>
                {
                    return !await repo.HasAbbAsync(abb);
                });
        }
    }

    public class UpdateMeasureUnitDTOValidator : AbstractValidator<UpdateMeasureUnitDTO>
    {
        public UpdateMeasureUnitDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(x => x.Name.Trim())
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Abbreviation.Trim())
                .NotEmpty().WithMessage("Abbreviation is required.")
                .MaximumLength(10).WithMessage("Abbreviation cannot exceed 10 characters.");
        }
    }
}
