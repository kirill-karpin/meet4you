using Infrastructure.Abstraction;

namespace User.Abstraction;

public interface IResetPasswordRepository : ICrudRepository<ResetPassword>
{
    //Task<List<User>> GetPagedAsync(int page, int itemsPerPage);
}