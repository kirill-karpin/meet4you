using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Dto;

namespace User.Service
{
    public class UserService : IUserService
    {
        public Task<UserDto> Add(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Update(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
