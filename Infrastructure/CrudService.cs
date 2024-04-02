using AutoMapper;
using Entities.Abstractions;
using Infrastructure.Abstraction;

namespace Infrastructure;

public abstract class CrudService<TEntity, TCreatingDtoEntity, TUpdatingDtoEntity, TDtoEntity>
    : ICrudService<TEntity, TCreatingDtoEntity, TUpdatingDtoEntity, TDtoEntity>
    where TDtoEntity : class
    where TUpdatingDtoEntity : class
    where TCreatingDtoEntity : class
    where TEntity : BaseEntity
{
    private readonly IMapper _mapper;
    private readonly ICrudRepository<TEntity> _crudRepository;

    public CrudService(
        IMapper mapper,
        ICrudRepository<TEntity> crudRepository)
    {
        _mapper = mapper;
        _crudRepository = crudRepository;
    }


    public async Task<ICollection<TDtoEntity>> GetPagedAsync(int page, int pageSize)
    {
        ICollection<TEntity> entities = await _crudRepository.GetPagedAsync(page, pageSize);
        return _mapper.Map<ICollection<TEntity>, ICollection<TDtoEntity>>(entities);
    }

    public async Task<TDtoEntity> GetByIdAsync(Guid id)
    {
        var entity = await _crudRepository.GetAsync(id);
        return _mapper.Map<TDtoEntity>(entity);
    }

    public async Task<Guid> CreateAsync(TCreatingDtoEntity creatingDtoEntity)
    {
        var entity = _mapper.Map<TCreatingDtoEntity, TEntity>(creatingDtoEntity);
        var createdEntity = await _crudRepository.AddAsync(entity);
        await _crudRepository.SaveChangesAsync();
        return createdEntity.Id;
    }

    public async Task UpdateAsync(Guid id, TUpdatingDtoEntity updatingDtoEntity)
    {
        var dbEntity = await _crudRepository.GetAsync(id);
        if (dbEntity == null)
        {
            throw new Exception($"Сущность с идентфикатором {id} не найдена");
        }

        var updateEntity = _mapper.Map<TUpdatingDtoEntity, TEntity>(updatingDtoEntity);
        _crudRepository.Update(updateEntity);
        await _crudRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _crudRepository.GetAsync(id);
        entity.Deleted = true;
        await _crudRepository.SaveChangesAsync();
    }

    public IQueryable<TEntity> GetAll(bool noTracking = false)
    {
        return _crudRepository.GetAll(noTracking);
    }
}