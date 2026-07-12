using Application.Base;
using Core.DTOs;

namespace Application.Contract
{
    public interface IRecipeCategoryService : IBaseService<RecipeCategoryDTO, CreateRecipeCategoryDTO, UpdateRecipeCategoryDTO>
    {
    }
}