using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface IIngredientService : IBaseService<IngredientDTO, CreateIngredientDTO, UpdateIngredientDTO>
    {
    }
}