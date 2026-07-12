using FluentValidation;

namespace Application.DTOs.Validators
{
    public class CreateMeasureUnitDTOValidator : AbstractValidator<CreateMeasureUnitDTO>
    {
        public CreateMeasureUnitDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Abbreviation)
                .NotEmpty().WithMessage("Abbreviation is required.")
                .MaximumLength(10).WithMessage("Abbreviation cannot exceed 10 characters.");
        }
    }

    public class UpdateMeasureUnitDTOValidator : AbstractValidator<UpdateMeasureUnitDTO>
    {
        public UpdateMeasureUnitDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Abbreviation)
                .NotEmpty().WithMessage("Abbreviation is required.")
                .MaximumLength(10).WithMessage("Abbreviation cannot exceed 10 characters.");
        }
    }
}
