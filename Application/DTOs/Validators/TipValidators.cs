using FluentValidation;

namespace Application.DTOs.Validators
{
    public class CreateTipDTOValidator : AbstractValidator<CreateTipDTO>
    {
        public CreateTipDTOValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(250).WithMessage("Content cannot exceed 250 characters.");
        }
    }

    public class UpdateTipDTOValidator : AbstractValidator<UpdateTipDTO>
    {
        public UpdateTipDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(250).WithMessage("Content cannot exceed 250 characters.");
        }
    }
}
