using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface IRecipe_IngredientService : IBaseService<Recipe_IngredientDTO, CreateRecipe_IngredientDTO, UpdateRecipe_IngredientDTO>
    {
    }
}