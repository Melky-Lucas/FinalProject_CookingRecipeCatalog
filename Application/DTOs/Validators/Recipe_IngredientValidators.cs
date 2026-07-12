using FluentValidation;

namespace Application.DTOs.Validators
{
    public class CreateRecipe_IngredientDTOValidator : AbstractValidator<CreateRecipe_IngredientDTO>
    {
        public CreateRecipe_IngredientDTOValidator()
        {
            RuleFor(x => x.RecipeId)
                .GreaterThan(0).WithMessage("RecipeId must be greater than zero.");

            RuleFor(x => x.IngredientId)
                .GreaterThan(0).WithMessage("IngredientId must be greater than zero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.UnitId)
                .GreaterThan(0).WithMessage("UnitId must be greater than zero.");
        }
    }

    public class UpdateRecipe_IngredientDTOValidator : AbstractValidator<UpdateRecipe_IngredientDTO>
    {
        public UpdateRecipe_IngredientDTOValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than zero.");

            RuleFor(x => x.RecipeId)
                .GreaterThan(0).WithMessage("RecipeId must be greater than zero.");

            RuleFor(x => x.IngredientId)
                .GreaterThan(0).WithMessage("IngredientId must be greater than zero.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            RuleFor(x => x.UnitId)
                .GreaterThan(0).WithMessage("UnitId must be greater than zero.");
        }
    }
}
