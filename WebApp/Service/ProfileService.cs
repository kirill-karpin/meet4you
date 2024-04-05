using Message.Abstraction;
using User;
using User.Dto;
using WebApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Install;
using Message.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using User.CustomExceprions;
using User.Abstraction;
using System.Reflection;
using System.Text;
using User.Settings;
using UserClass = User.User; // позволяет обращаться к классу User через UserClass, вместо User.User (ужас!)
using System.Security.Cryptography;


namespace WebApp.Service;

public class ProfileService : IProfileService
{
    private readonly DataContext _db;
    private readonly IUserService _userService;
    private readonly IMessageService _messageService;
    private readonly IMapper _userMapper;
    private readonly IUserRepository _userRepository;
    private readonly IResetPasswordRepository _resetPasswordRepository;

    public ProfileService(IUserService userService, IMessageService messageService, DataContext db,
        IMapper mapper, IUserRepository userRepository, IResetPasswordRepository resetPassRepository)
    {
        _db = db;
        _messageService = messageService;
        _userService = userService;
        _userMapper = mapper;
        _userRepository = userRepository;
        _resetPasswordRepository = resetPassRepository;
    }
    public async Task<List<UserProfile>> ListProfile(GetProfilesListQuery query, int itemsPerPage, int page)
    {
        var userFilterDto = new UserFilterDto
        {
            CityId = query.CityId,
            CountryId = query.CountryId,
            FamilyStatus = query.FamilyStatus,
            Gender = query.Gender,
            HaveChildren = query.HaveChildren,
            AgeFrom = query.AgeFrom,
            AgeTo = query.AgeTo
        };

        var users = await _userService.GetPagedAsync(userFilterDto, itemsPerPage, page);

        return await Task.FromResult(new List<UserProfile>(users.Select(u => new UserProfile { Id = u.Id , User=u})));
    }

    public async Task<IProfile> GetProfile(Guid id)
    {
        var user = await _userRepository.GetAsync(id);

        if (user==null)
            throw new UserNotFoundException("user not found");

        return await Task.FromResult<IProfile>(new UserProfile()
        {
            Id = id,
            User = _userMapper.Map<UserClass, UserDto>(user)
            
        });
    }

    public async Task<ISignInResponseDto> AuthByLoginPassword(SignInModelDto signInModelDto)
    {
        var identity = await GetIdentity(signInModelDto);
        if (identity == null)
        {
            return new SignInResponseErrorDto()
            {
                Result = "Invalid username or password."
            };
        }

        var now = DateTime.UtcNow;
        return new SignInResponseDto()
        {
            Token = GetJwt(now, identity, AuthOptions.AuthOptions.GetSymmetricSecurityKey(), AuthOptions.AuthOptions.LifetimeAccesToken),
            RefreshToken = GetJwt(now, identity, AuthOptions.AuthOptions.GetSymmetricSecurityRefreshKey(), AuthOptions.AuthOptions.LifetimeRefreshToken)
        };
    }

    public async Task<UserDto> Registration(RegistrationRequestModelDto registrationRequestModel)
    {
        return  await this.Add(new UserDto_WithLoginPassword()
        {
            Id =  Guid.NewGuid(),
            About = registrationRequestModel.About,
            Blocked = false,
            Gender = registrationRequestModel.Gender,
            Login = registrationRequestModel.Login,
            FirstName = registrationRequestModel.FirstName,
            LastName = registrationRequestModel.LastName,
            Password = registrationRequestModel.Password,
            Surname = registrationRequestModel.Surname,
            DateOfBirth = registrationRequestModel.DateOfBirth,
            LookingFor = registrationRequestModel.LookingFor,
            FamilyStatus = registrationRequestModel.FamilyStatus,
            HaveChildren = registrationRequestModel.HaveChildren,
            Email = registrationRequestModel.Email,
            UserName = registrationRequestModel.Login
            
        });
    }

    private async Task<ClaimsIdentity?> GetIdentity(SignInModelDto signInModel)
    {
        User.User? user = await _db.Users.FirstOrDefaultAsync(x => x.Login == signInModel.Login && x.Password == signInModel.Password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString())
            };
             
            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
        
        return null;
    }

    private string GetJwt(DateTime now, ClaimsIdentity identity, SymmetricSecurityKey key, int lifeTime)
    {
        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.AuthOptions.Issuer,
            audience: AuthOptions.AuthOptions.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(lifeTime)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));
        
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }

    async public Task<IPersonalProfile>  GetPersonaProfile(Guid id)
    {
        UserDto user = await this.Get(id);
        ICollection<MessageDto> messages = await _messageService.GetPagedAsync(1, 10);
        var profile = new PersonalProfile();
        profile.Id = id;
        profile.User = user;
        profile.Messages = messages;
        return profile;
    }

    // тут начинаются методы с UserService
    public async Task<UserDto> Add(UserDto_WithLoginPassword userDto_WithLoginPassword)
    {
        // Гененрим id для userDto_WithLoginPassword
        userDto_WithLoginPassword.Id = Guid.NewGuid();

        UserClass newUser = _userMapper.Map<UserClass>(userDto_WithLoginPassword);
        UserClass addedUser = _userRepository.Add(newUser);
        await _userRepository.SaveChangesAsync();

        // Проверяем, что User был успешно добавлен - получаем его из БД
        UserClass getNewAddedUser = await _userRepository.GetAsync(addedUser.Id);

        if (getNewAddedUser is null)
            throw new UserNotFoundException("Не найден добавленный пользователь. Обратитесь к администраторам.");

        UserDto userDto = _userMapper.Map<UserDto>(getNewAddedUser);

        return userDto;
    }

    public async Task<UserDto> Get(Guid id)
    {
        UserClass user = await _userRepository.GetAsync(id);
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
        UserClass userFromDb = null;
        if (userDto.Id != Guid.Empty)
            userFromDb = await _userRepository.GetAsync(userDto.Id);

        if (userFromDb is null) throw new UserNotFoundException("Пользователь не найден, так как не передано поле Id");

        UserClass userFromDbo = _userMapper.Map<UserClass>(userDto);
        CopyProperties(userFromDbo, userFromDb);
        await _userRepository.SaveChangesAsync();

        UserDto resultUserDto = _userMapper.Map<UserDto>(userFromDb);

        return resultUserDto;
    }

    private void CopyProperties(UserClass source, UserClass destination)
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
        var userFromDB = await _userRepository.GetAsync(userId);
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

        var userFromDB = await _userRepository.GetAsync(userId);
        userFromDB.Password = passHash;
        await _userRepository.SaveChangesAsync();

        return true;

    }

    // DELETE AFTER
    public async Task<List<UserDto>> GetTest()
    {
        var usersFromDb = await _userRepository.FindAsync(x => x.Login == "Ivan" && x.Age == 22);
        List<UserDto> userDtos = new List<UserDto>();
        foreach (var user in usersFromDb)
        {
            UserDto userDto = _userMapper.Map<UserDto>(user);
            userDtos.Add(userDto);
        }
        return userDtos;
    }

    public async Task AddFakeUsers()
    {
        await _userService.AddFakeUsers();
    }
}