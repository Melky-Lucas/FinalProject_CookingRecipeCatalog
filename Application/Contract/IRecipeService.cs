using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface IRecipeService : IBaseService<RecipeDTO, CreateRecipeDTO, UpdateRecipeDTO>
    {
    }
}
