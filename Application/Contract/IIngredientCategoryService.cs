using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface IIngredientCategoryService : IBaseService<IngredientCategoryDTO, CreateIngredientCategoryDTO, UpdateIngredientCategoryDTO>
    {
    }
}