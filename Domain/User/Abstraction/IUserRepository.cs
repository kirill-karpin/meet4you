using Infrastructure.Abstraction;

namespace User.Abstraction;

public interface IUserRepository : ICrudRepository<User>
{
    //Task<List<User>> GetPagedAsync(int page, int itemsPerPage);
}