using FluentValidation;

namespace Application.DTOs.Validators
{
    public class CreateRecipeDTOValidator : AbstractValidator<CreateRecipeDTO>
    {
        public CreateRecipeDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Desription is required.")
                .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(250).WithMessage("ImageURL cannot exceed 250 characters.");

            RuleFor(x => x.PreparationTime)
                .GreaterThan(TimeSpan.Zero).WithMessage("Preparation time must be greater than zero.");

            RuleFor(x => x.CookingTime)
                .GreaterThan(TimeSpan.Zero).WithMessage("Cooking time must be greater than zero.");

            RuleFor(x => x.Servings)
                .GreaterThan(0).WithMessage("Servings must be greater than zero.");

            RuleFor(x => x.Calories)
                .GreaterThanOrEqualTo(0).WithMessage("Calories must be greater than or equal to zero.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than zero.");

            RuleFor(x => x.Category_Ids)
                .NotNull().WithMessage("Category IDs cannot be null.")
                .Must(ids => ids.All(id => id > 0)).WithMessage("All Category IDs must be greater than zero.");

            RuleForEach(x => x.Recipe_Ingredients)
                .NotNull().WithMessage("Recipe ingredients cannot be null.")
                .SetValidator(new CreateRecipe_IngredientDTOValidator());

            RuleForEach(x => x.CookingSteps)
                .NotNull().WithMessage("Cooking steps cannot be null.")
                .SetValidator(new CreateCookingStepDTOValidator());

            RuleForEach(x => x.Tips)
                .NotNull().WithMessage("Tips cannot be null.")
                .SetValidator(new CreateTipDTOValidator());
        }

        public class UpdateRecipeDTOValidator : AbstractValidator<UpdateRecipeDTO>
        {
            public UpdateRecipeDTOValidator()
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0).WithMessage("Id must be greater than zero.");

                RuleFor(x => x.Title)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

                RuleFor(x => x.Description)
                    .NotEmpty().WithMessage("Desription is required.")
                    .MaximumLength(250).WithMessage("Description cannot exceed 250 characters.");

                RuleFor(x => x.ImageUrl)
                    .MaximumLength(250).WithMessage("ImageURL cannot exceed 250 characters.");

                RuleFor(x => x.PreparationTime)
                    .GreaterThan(TimeSpan.Zero).WithMessage("Preparation time must be greater than zero.");

                RuleFor(x => x.CookingTime)
                    .GreaterThan(TimeSpan.Zero).WithMessage("Cooking time must be greater than zero.");

                RuleFor(x => x.Servings)
                    .GreaterThan(0).WithMessage("Servings must be greater than zero.");

                RuleFor(x => x.Calories)
                    .GreaterThanOrEqualTo(0).WithMessage("Calories must be greater than or equal to zero.");

                RuleFor(x => x.Category_Ids)
                    .NotNull().WithMessage("Category IDs cannot be null.")
                    .Must(ids => ids.All(id => id > 0)).WithMessage("All Category IDs must be greater than zero.");

                RuleForEach(x => x.Recipe_Ingredients)
                    .NotNull().WithMessage("Recipe ingredients cannot be null.")
                    .SetValidator(new UpdateRecipe_IngredientDTOValidator());
            }

        }
    }
}
