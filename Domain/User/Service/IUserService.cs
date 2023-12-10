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
        public Task<UserDto> Add(UserDto userDto);
        public Task<UserDto> Update(UserDto userDto);
    }
}
