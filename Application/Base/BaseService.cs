using Application.Contract;
using Application.Interfaces;
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
        protected readonly IServiceProvider _serviceProvider;
        protected readonly IApplicationValidator _validator;
        protected abstract IGenericRepository<TEntity> Repository { get; }

        protected BaseService(IUnitOfWork unitOfWork, IObjectMapper mapper, IServiceProvider serviceProvider, IApplicationValidator Validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _validator = Validator;
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
                return ServiceResult<TDto>.Failure("Entity not found.", 404);

            return ServiceResult<TDto>.Success(_mapper.Map<TEntity, TDto>(entity));
        }

        public virtual async Task<ServiceResult<TDto>> CreateAsync(TCreateDto dto)
        {
            await _validator.ValidateAsync(dto);

            var entity = _mapper.Map<TCreateDto, TEntity>(dto);
            Repository.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<TDto>.Success(_mapper.Map<TEntity, TDto>(entity), statusCode: 201);
        }

        public virtual async Task<ServiceResult<TDto>> UpdateAsync(int id, TUpdateDto dto)
        {
            await _validator.ValidateAsync(dto);

            if (!await Repository.ExistsAsync(id))
                return ServiceResult<TDto>.Failure("Entity not found.", 404);

            TEntity entity = _mapper.Map<TUpdateDto, TEntity>(dto);
            entity.Id = id;

            Repository.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<TDto>.Success(_mapper.Map<TEntity, TDto>(entity));
        }

        public virtual async Task<ServiceResult> DeleteAsync(int id)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity is null)
                return ServiceResult.Failure("Entity not found.", 404);
            Repository.Delete(entity);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(statusCode: 204);
        }
    }
}
