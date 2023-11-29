using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace User.Mappers
{
    public class UserMapper : IUserMapper<UserDto, Entities.User>
    {
        /// <summary>
        /// Преобразует UserDto в UserEntity
        /// </summary>
        /// <param name="t">UserDto</param>
        /// <returns>UserEntity</returns>
        public async Task<Entities.User> Cast(UserDto t)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Преобразует UserEntity в UserDto
        /// </summary>
        /// <param name="u">UserEntity</param>
        /// <returns>UserDto</returns>
        public async Task<UserDto> Cast(Entities.User u)
        {
            throw new NotImplementedException();
        }
    }
}
