using Application.Base;
using Core.DTOs;

namespace Application.Contract
{
    public interface ITipService : IBaseService<RecipeTipDTO, CreateTipDTO, UpdateTipDTO>
    {
    }
}