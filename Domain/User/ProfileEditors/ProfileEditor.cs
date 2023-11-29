using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.ProfileEditors
{
    internal class ProfileEditor : IProfileEditor<UserDto, Entities.User>
    {
        /// <summary>
        /// Сохраняет настройки пользователя
        /// </summary>
        /// <param name="userDto">UserDto с изменёнными данными профиля</param>
        /// <returns>UserEntity, как подтверждение того, что настройки были применены</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Entities.User> ChangeProfileSettings(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
