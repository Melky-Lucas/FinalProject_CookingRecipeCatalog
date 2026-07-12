using Application.Base;
using Core.DTOs;

namespace Application.Contract
{
    public interface IIngredientService : IBaseService<IngredientDTO, CreateIngredientDTO, UpdateIngredientDTO>
    {
    }
}