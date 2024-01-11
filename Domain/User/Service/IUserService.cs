using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Dto;

namespace User
{
    public interface IUserService
    {
        public Task<UserDto> Add(UserDto_WithLoginPassword userDto_WithLoginPassword);
        public Task<UserDto> Get(Guid id);
        public Task<string> GetHashPasswordWithSalt(string unhashedPassword, string salt);
    }
}