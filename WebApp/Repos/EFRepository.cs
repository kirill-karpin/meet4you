using Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Repos;

public class EFRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly DataContext _dataContext;

    public EFRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }


    public async Task<T?> GetAsync(Guid id)
    {
        return await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }
}