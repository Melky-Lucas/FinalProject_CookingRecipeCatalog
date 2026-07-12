using Application.Base;
using Core.DTOs;

namespace Application.Contract
{
    public interface IIngredientCategoryService : IBaseService<IngredientCategoryDTO, CreateIngredientCategoryDTO, UpdateIngredientCategoryDTO>
    {
    }
}