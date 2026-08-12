using FluentValidation;

namespace Application.DTOs.Validators
{
    public class CreateCookingStepDTOValidator : AbstractValidator<CreateRecipeStepDTO>
    {
        public CreateCookingStepDTOValidator()
        {
            RuleFor(x => x.StepNumber)
                .GreaterThan(0).WithMessage("Step number must be greater than zero.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.EstimatedDuration)
                .GreaterThan(TimeSpan.Zero).WithMessage("Estimated duration must be greater than zero.");

            RuleFor(x => x.Instruction)
                .NotEmpty().WithMessage("Instruction is required.")
                .MaximumLength(250).WithMessage("Instruction cannot exceed 250 characters.");
        }
    }

    public class UpdateCookingStepDTOValidator : AbstractValidator<UpdateRecipeStepDTO>
    {
        public UpdateCookingStepDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(x => x.StepNumber)
                .GreaterThan(0).WithMessage("Step number must be greater than zero.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

            RuleFor(x => x.EstimatedDuration)
                .GreaterThan(TimeSpan.Zero).WithMessage("Estimated duration must be greater than zero.");

            RuleFor(x => x.Instruction)
                .NotEmpty().WithMessage("Instruction is required.")
                .MaximumLength(250).WithMessage("Instruction cannot exceed 250 characters.");
        }
    }
}
