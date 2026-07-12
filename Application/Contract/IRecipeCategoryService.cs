using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface IRecipeCategoryService : IBaseService<RecipeCategoryDTO, CreateRecipeCategoryDTO, UpdateRecipeCategoryDTO>
    {
    }
}