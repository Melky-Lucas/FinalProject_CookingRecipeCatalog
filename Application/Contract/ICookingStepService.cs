using Application.Base;
using Core.DTOs;

namespace Application.Contract
{
    public interface ICookingStepService : IBaseService<RecipeCookingStepDTO, CreateCookingStepDTO, UpdateCookingStepDTO>
    {
    }
}