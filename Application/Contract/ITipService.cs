using Application.Base;
using Application.DTOs;

namespace Application.Contract
{
    public interface ITipService : IBaseService<RecipeTipDTO, CreateTipDTO, UpdateTipDTO>
    {
    }
}