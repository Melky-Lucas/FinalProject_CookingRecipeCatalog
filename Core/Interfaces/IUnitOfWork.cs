using Core.Interfaces.Repositories;

namespace Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRecipeRepository Recipes { get; }
        IUserRepository Users { get; }
        IIngredientRepository Ingredients { get; }
        ICookingStepRepository CookingSteps { get; }
        ITipRepository TipRepository { get; }
        IRecipeCategoryRepository RecipeCategories { get; }
        IRecipe_CategoryRepository Recipe_Category { get; }
        IRecipe_IngredientRepository Recipe_Ingredients { get; }
        IIngredientCategoryRepository IngredientCategories { get; }
        IPasswordRepository Passwords { get; }
        IMeasureUnitRepository MeasureUnits { get; }
        IRoleRepository Roles { get; }
        Task<int> SaveChangesAsync();
    }
}
