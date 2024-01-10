using AutoMapper;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using User.Abstraction;
using User.CustomExceprions;
using User.Dto;

namespace User.Service
{
    public class UserService : CrudService<User, UserDto_WithLoginPassword, UserDto_WithLoginPassword, UserDto>, IUserService
    {
        private readonly IMapper _userMapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository) : base(mapper, userRepository)
        {
            _userMapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Add(UserDto_WithLoginPassword userDto_WithLoginPassword)
        {
            // Гененрим id для userDto_WithLoginPassword
            userDto_WithLoginPassword.Id = Guid.NewGuid();

            User newUser = _userMapper.Map<User>(userDto_WithLoginPassword);
            User addedUser = _userRepository.Add(newUser);
            await _userRepository.SaveChangesAsync();

            // Проверяем, что User был успешно добавлен - получаем его из БД
            User getNewAddedUser = await _userRepository.GetAsync(addedUser.Id);

            if (getNewAddedUser is null)
                throw new UserNotFoundException("Не найден добавленный пользователь. Обратитесь к администраторам.");

            UserDto userDto = _userMapper.Map<UserDto>(getNewAddedUser);

            return userDto;
        }

        public async Task<UserDto> Get(Guid id)
        {
            User user = await _userRepository.GetAsync(id);
            UserDto userDto = _userMapper.Map<UserDto>(user);

            return userDto;
        }

        /// <summary>
        /// Генерирует Хэш на основе пароля пользователя и строки Соли из файла конфигурации
        /// </summary>
        /// <param name="unhashedPassword">Нехешированный (оригинальный и неизмененный) пароль</param>
        /// <returns>Хэш паролья для последующего хранения в БД</returns>
        public async Task<string> GetHashPasswordWithSalt(string unhashedPassword, string salt)
        {
            string preResult = $"{unhashedPassword}{salt}";
            string result = string.Empty;

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(preResult));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                result = builder.ToString();
            }

            return result;
        }
    }
}
