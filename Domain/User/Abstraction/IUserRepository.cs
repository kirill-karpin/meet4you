using Infrastructure.Abstraction;
using User.Dto;

namespace User.Abstraction;

public interface IUserRepository : ICrudRepository<User>
{
    //Task<List<User>> GetPagedAsync(int page, int itemsPerPage);
    Task<List<User>> GetPagedAsync(UserFilterDto userFilterDto);
}