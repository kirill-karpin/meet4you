using AutoMapper;
using Infrastructure;
using Location;
using Location.UserLocation.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using User.Abstraction;
using User.CustomExceprions;
using User.Dto;
using User.Settings;

namespace User.Service
{
    public class UserService : CrudService<User, UserDto_WithLoginPassword, UserDto_WithLoginPassword, UserDto>, IUserService
    {
        private readonly IMapper _userMapper;
        private readonly IUserRepository _userRepository;
        private readonly IResetPasswordRepository _resetPasswordRepository;
        private readonly ILocationService _locationService;

        public UserService(IMapper mapper, IUserRepository userRepository, IResetPasswordRepository resetPassRepository, ILocationService locationService) : base(mapper, userRepository)
        {
            _userMapper = mapper;
            _userRepository = userRepository;
            _resetPasswordRepository = resetPassRepository;
            _locationService = locationService;
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
        /// Обновляет данные о пользователе
        /// </summary>
        /// <param name="userDto">UserDto с обновлённой информацией</param>
        /// <returns>UserDto с обновленными данными, чтобы их можно было отобразить пользователю</returns>
        /// <exception cref="UserNotFoundException">Если в UserDto не было заполнено поле Id, то метод выдаст данную ошибку</exception>
        public async Task<UserDto> Update(UserDto userDto)
        {
            User userFromDb = null;
            if (userDto.Id != Guid.Empty)
                userFromDb = await _userRepository.GetAsync(userDto.Id);

            if (userFromDb is null) throw new UserNotFoundException("Пользователь не найден, так как не передано поле Id");

            User userFromDbo = _userMapper.Map<User>(userDto);
            CopyProperties(userFromDbo, userFromDb);
            await _userRepository.SaveChangesAsync();

            UserDto resultUserDto = _userMapper.Map<UserDto>(userFromDb);

            return resultUserDto;
        }

        private void CopyProperties(User source, User destination)
        {
            PropertyInfo[] destinationProperties = destination.GetType().GetProperties();
            foreach (PropertyInfo destinationPi in destinationProperties)
            {
                PropertyInfo sourcePi = source.GetType().GetProperty(destinationPi.Name);
                // если не отбрасывать null-ы, то мы затрем какое-то поле, которое помечено как "Not Null" на уровне БД - получим Exception и своё грустное лицо в подарок
                if (sourcePi.GetValue(source, null) != null)
                    destinationPi.SetValue(destination, sourcePi.GetValue(source, null), null);
            }
        }


        /// <summary>
        /// Генерирует Хэш на основе пароля пользователя и строки Соли из файла конфигурации
        /// </summary>
        /// <param name="unhashedPassword">Нехешированный (оригинальный и неизмененный) пароль</param>
        /// <returns>Хэш паролья для последующего хранения в БД</returns>
        public async Task<string> GetHashPasswordWithSalt(string unhashedPassword, string salt = null)
        {
            if (salt is null)
            {
                salt = new SettingsReader().GetSalt();
            }

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

        public async Task<bool> AddRequestToChangePassword(Guid userId, string newPassword)
        {
            var user = await _userRepository.GetAsync(userId);

            string conformationCode = new Random().Next(100000, 999999).ToString();

            await _resetPasswordRepository.AddAsync(
                new ResetPassword
                {
                    Login = user.Login,
                    ConfirmationCode = conformationCode,
                    TimeLimit = DateTime.Now.AddMinutes(30),
                    CreatedAt = DateTime.Now,
                    NewPasswordHash = string.Empty // для начала - просто добавляем новый запрос. Сам хэш нового пароля появится позже
                }
                );

            await _resetPasswordRepository.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Возвращает код подтверждения
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Код подтверждения, если всё ок. Иначе - String.Empty</returns>
        public async Task<string> GetConfirmationCode(Guid userId)
        {
            User userFromDB = await _userRepository.GetAsync(userId);
            UserDto userDto = _userMapper.Map<UserDto>(userFromDB);

            var userResetPassList = await _resetPasswordRepository.FindAsync(x => x.Login == userDto.Login);
            if (userResetPassList == null) return string.Empty;
            // отсуда переделать на DTO
            var resetCodeRecord = userResetPassList.Where(x => x.CreatedAt == userResetPassList.Max(d => d.CreatedAt)).OrderByDescending(x => x.CreatedAt).FirstOrDefault();

            ResetPasswordDto resetPasswordDto = _userMapper.Map<ResetPasswordDto>(resetCodeRecord);
            // Проверяем, действует ли ещё данный код
            if (DateTime.Now < resetPasswordDto.TimeLimit)
                return resetPasswordDto.ConfirmationCode;

            return string.Empty;
        }

        public async Task<bool> ChangePassword(Guid userId, string newPassword, string confirmationCode)
        {
            string confirmationCodeFromDB = await GetConfirmationCode(userId);
            if (confirmationCodeFromDB != confirmationCode)
                return false;

            string passHash = await GetHashPasswordWithSalt(newPassword);

            User userFromDB = await _userRepository.GetAsync(userId);
            userFromDB.Password = passHash;
            await _userRepository.SaveChangesAsync();

            return true;

        }
        public async Task<List<UserDto>> GetPagedAsync(UserFilterDto userFilterDto, int itemsPerPage, int page)
        {
            List<UserDto> users = _userMapper.Map<List<User>, List<UserDto>>(await _userRepository.GetPagedAsync(userFilterDto, itemsPerPage, page));
           
            users.ForEach( u =>
            {
                DateTime today = DateTime.Today;
                var age = today.Year - u.DateOfBirth.Year;
                if (u.DateOfBirth.AddYears(age) > today)
                    age--;
                u.Age = age;

                u.Location = _locationService.GetUserLocation(u.Id).Result;
            });

            return users;
        }


    }
}