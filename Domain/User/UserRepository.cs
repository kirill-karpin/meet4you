using Infrastructure;
using Microsoft.EntityFrameworkCore;
using User.Abstraction;
using User.Dto;

namespace User
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<User>> GetPagedAsync(UserFilterDto userFilterDto)
        {
            var query = GetAll().AsQueryable();

            if (userFilterDto.Gender.HasValue)
            {
                query = query.Where(u=>u.Gender == userFilterDto.Gender.Value);
            }
            if (userFilterDto.FamilyStatus.HasValue)
            {
                query = query.Where(u => u.FamilyStatus == userFilterDto.FamilyStatus.Value);
            }
            if (userFilterDto.HaveChildren.HasValue)
            {
                query = query.Where(u => u.HaveChildren == userFilterDto.HaveChildren.Value);
            }

            query = query
                .Skip((userFilterDto.Page - 1) * userFilterDto.ItemsPerPage)
                .Take(userFilterDto.ItemsPerPage);

            return query.ToList();
        }
    }
}