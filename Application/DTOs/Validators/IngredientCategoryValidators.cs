using FluentValidation;

namespace Application.DTOs.Validators
{
    public class CreateIngredientCategoryDTOValidator : AbstractValidator<CreateIngredientCategoryDTO>
    {
        public CreateIngredientCategoryDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");
        }


        public class UpdateIngredientCategoryDTOValidator : AbstractValidator<UpdateIngredientCategoryDTO>
        {
            public UpdateIngredientCategoryDTOValidator()
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0).WithMessage("Id must be greater than zero.");

                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

                RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Description is required.")
                    .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");
            }
        }
    }
}