using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface ICookingStepService : IBaseService<RecipeCookingStepDTO, CreateCookingStepDTO, UpdateCookingStepDTO>
    {
    }
}