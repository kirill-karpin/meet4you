using Entities.Abstractions;

namespace Infrastructure.Abstraction;

public interface ICrudService<TEntity, in TCreatingDtoEntity, in TUpdatingDtoEntity, TDtoEntity>
    where TCreatingDtoEntity : class
    where TDtoEntity : class
    where TEntity : class
{
    /// <summary>
    /// Получить список сущностей.
    /// </summary>
    /// <param name="page"> Номер страницы. </param>
    /// <param name="pageSize"> Объем страницы. </param>
    /// <returns> Список курсов. </returns>
    public Task<ICollection<TDtoEntity>> GetPagedAsync(int page, int pageSize);

    /// <summary>
    /// Получить сущность.
    /// </summary>
    /// <param name="id"> Идентификатор. </param>
    /// <returns> ДТО курса. </returns>
    public Task<TDtoEntity> GetByIdAsync(Guid id);


    /// <summary>
    /// Создать сущность.
    /// </summary>
    /// <param name="creatingDtoEntity"> ДТО создаваемой сущности. </param>
    /// <returns> Идентификатор. </returns>
    public Task<Guid> CreateAsync(TCreatingDtoEntity creatingDtoEntity);


    /// <summary>
    /// Изменить сущность.
    /// </summary>
    /// <param name="id"> Идентификатор. </param>
    /// <param name="updatingDtoEntity"> ДТО редактируемой сущности. </param>
    public Task UpdateAsync(Guid id, TUpdatingDtoEntity updatingDtoEntity);

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="id"> Идентификатор. </param>
    public Task DeleteAsync(Guid id);
    
    public IQueryable<TEntity> GetAll(bool noTracking = false);
}