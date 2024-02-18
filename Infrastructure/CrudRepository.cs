using Entities.Abstractions;
using Infrastructure.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;

namespace Infrastructure;

public class CrudRepository<TEntity> : ICrudRepository<TEntity> where TEntity : BaseEntity
{
    DbContext _context;
    private readonly DbSet<TEntity> _entitySet;

    public CrudRepository(DbContext context)
    {
        _context = context;
        _entitySet = _context.Set<TEntity>();
    }
    
    #region Get

    /// <summary>
    /// Получить сущность по ID.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <returns> Cущность. </returns>
    public virtual TEntity Get(Guid id)
    {
        return _entitySet.Find(id);
    }

    /// <summary>
    /// Получить сущность по Id.
    /// </summary>
    /// <param name="id"> Id сущности. </param>
    /// <returns> Cущность. </returns>
    public virtual async Task<TEntity> GetAsync(Guid id)
    {
        return await _entitySet.FindAsync((object)id);
    }

    #endregion

    #region GetAll

    /// <summary>
    /// Запросить все сущности в базе.
    /// </summary>
    /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
    /// <returns> IQueryable массив сущностей. </returns>
    public virtual IQueryable<TEntity> GetAll(bool asNoTracking = false)
    {
        return asNoTracking ? _entitySet.AsNoTracking() : _entitySet;
    }

    /// <summary>
    /// Запросить все сущности в базе.
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <param name="asNoTracking"> Вызвать с AsNoTracking. </param>
    /// <returns> Список сущностей. </returns>
    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, bool asNoTracking = false)
    {
        return await GetAll().ToListAsync(cancellationToken);
    }

    #endregion

    #region FindAsync
    public virtual async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Where(expression).ToListAsync(cancellationToken);
    }
    #endregion

    #region Create

    /// <summary>
    /// Добавить в базу сущность.
    /// </summary>
    /// <param name="entity"> Cущность для добавления. </param>
    /// <returns>. Добавленная сущность. </returns>
    public virtual TEntity Add(TEntity entity)
    {
        var objToReturn = _entitySet.Add(entity);
        return objToReturn.Entity;
    }

    /// <summary>
    /// Добавить в базу одну сущность.
    /// </summary>
    /// <param name="entity"> Сущность для добавления. </param>
    /// <returns> Добавленная сущность. </returns>
    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        return (await _entitySet.AddAsync(entity)).Entity;
    }

    /// <summary>
    /// Добавить в базу массив сущностей.
    /// </summary>
    /// <param name="entities"> Массив сущностей. </param>
    public virtual void AddRange(List<TEntity> entities)
    {
        var enumerable = entities as IList<TEntity> ?? entities.ToList();
        _entitySet.AddRange(enumerable);
    }

    /// <summary>
    /// Добавить в базу массив сущностей.
    /// </summary>
    /// <param name="entities"> Массив сущностей. </param>
    public virtual async Task AddRangeAsync(ICollection<TEntity> entities)
    {
        if (entities == null || !entities.Any())
        {
            return;
        }

        await _entitySet.AddRangeAsync(entities);
    }

    #endregion

    #region Update

    /// <summary>
    /// Для сущности проставить состояние - что она изменена.
    /// </summary>
    /// <param name="entity"> Сущность для изменения. </param>
    public virtual void Update(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    #endregion

    #region Delete

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="id"> Id удалённой сущности. </param>
    /// <returns> Была ли сущность удалена. </returns>
    public virtual bool Delete(Guid id)
    {
        var obj = _entitySet.Find(id);
        if (obj == null)
        {
            return false;
        }

        _entitySet.Remove(obj);
        return true;
    }

    /// <summary>
    /// Удалить сущность.
    /// </summary>
    /// <param name="entity"> Сущность для удаления. </param>
    /// <returns> Была ли сущность удалена. </returns>
    public virtual bool Delete(TEntity entity)
    {
        if (entity == null)
        {
            return false;
        }

        _context.Entry(entity).State = EntityState.Deleted;
        return true;
    }

    /// <summary>
    /// Удалить сущности.
    /// </summary>
    /// <param name="entities"> Коллекция сущностей для удаления. </param>
    /// <returns> Была ли операция завершена успешно. </returns>
    public virtual bool DeleteRange(ICollection<TEntity> entities)
    {
        if (entities == null || !entities.Any())
        {
            return false;
        }

        _entitySet.RemoveRange(entities);
        return true;
    }

    #endregion

    #region SaveChanges

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    public virtual void SaveChanges()
    {
        _context.SaveChanges();
    }

    /// <summary>
    /// Сохранить изменения.
    /// </summary>
    public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    #endregion



    public async Task<List<TEntity>> GetPagedAsync(int page, int itemsPerPage)
    {
        var query = GetAll();
        return await query
            .Skip((page - 1) * itemsPerPage)
            .Take(itemsPerPage)
            .ToListAsync();
    }
}