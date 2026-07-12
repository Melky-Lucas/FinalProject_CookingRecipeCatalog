using Core.Base;
using Core.Interfaces;
using Core.Interfaces.Repositories.Generic;

namespace Application.Base
{
    public abstract class BaseService<TEntity, TDto, TCreateDto, TUpdateDto>
        : IBaseService<TDto, TCreateDto, TUpdateDto>
        where TEntity : BaseEntity
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IObjectMapper _mapper;
        protected abstract IGenericRepository<TEntity> Repository { get; }

        protected BaseService(IUnitOfWork unitOfWork, IObjectMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public virtual async Task<ServiceResult<IEnumerable<TDto>>> GetAllAsync()
        {
            var entities = await Repository.GetAllAsync();
            var dtos = entities.Select(e => _mapper.Map<TEntity, TDto>(e));
            return ServiceResult<IEnumerable<TDto>>.Success(dtos);
        }

        public virtual async Task<ServiceResult<TDto>> GetByIdAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity is null)
                return ServiceResult<TDto>.Failure("Entidad no encontrada", 404);

            return ServiceResult<TDto>.Success(_mapper.Map<TEntity, TDto>(entity));
        }

        public virtual async Task<ServiceResult<TDto>> CreateAsync(TCreateDto dto)
        {
            var entity = _mapper.Map<TCreateDto, TEntity>(dto);
            Repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<TDto>.Success(_mapper.Map<TEntity, TDto>(entity));
        }

        public virtual async Task<ServiceResult<TDto>> UpdateAsync(int id, TUpdateDto dto)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity is null)
                return ServiceResult<TDto>.Failure("Entidad no encontrada", 404);
            Repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult<TDto>.Success(_mapper.Map<TEntity, TDto>(entity));
        }

        public virtual async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity is null)
                return ServiceResult.Failure("Entidad no encontrada", 404);
            Repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();
            return ServiceResult.Success();
        }
    }
}
