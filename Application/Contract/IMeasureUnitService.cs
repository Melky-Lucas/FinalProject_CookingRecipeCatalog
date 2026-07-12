using Application.Base;
using Core.DTOs;

namespace Application.Contract
{
    public interface IMeasureUnitService : IBaseService<MeasureUnitDTO, CreateMeasureUnitDTO, UpdateMeasureUnitDTO>
    {
    }
}