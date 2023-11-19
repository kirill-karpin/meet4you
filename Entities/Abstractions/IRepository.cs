namespace Entities.Abstractions;

public interface IRepository<T> where T : BaseEntity
{
    public Task<T?> GetAsync(Guid id);
}