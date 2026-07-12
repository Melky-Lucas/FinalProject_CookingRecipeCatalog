using Application.Base;
using Core.DTOs;

namespace Application.Contract
{
    public interface IRecipeService : IBaseService<RecipeDTO, CreateRecipeDTO, UpdateRecipeDTO>
    {
    }
}
