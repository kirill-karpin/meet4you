using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Dto;

namespace User.ProfileEditors
{
    internal class ProfileEditor : IProfileEditor<UserDto, User>
    {
        /// <summary>
        /// Сохраняет настройки пользователя
        /// </summary>
        /// <param name="userDto">UserDto с изменёнными данными профиля</param>
        /// <returns>UserEntity, как подтверждение того, что настройки были применены</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<User> ChangeProfileSettings(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
