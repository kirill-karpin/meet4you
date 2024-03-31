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

        public async Task<List<User>> GetPagedAsync(UserFilterDto userFilterDto, int itemsPerPage, int page)
        {
            var query = GetAll().AsQueryable();

            query = query.Where(u => userFilterDto.AgeFrom <= DateTime.Today.Year - u.DateOfBirth.Year && DateTime.Today.Year - u.DateOfBirth.Year <= userFilterDto.AgeTo);

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
            if (userFilterDto.CountryId.HasValue)
            {
                query = query
                //.Include(u => u.Location)
                .Where(u => u.Location.CountryId == userFilterDto.CountryId.Value);
            }
            if (userFilterDto.CityId.HasValue)
            {
                query = query.Where(u => u.Location.CityId == userFilterDto.CityId);
            }

            query = query
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage);

            return await query.ToListAsync();
        }
    }
}