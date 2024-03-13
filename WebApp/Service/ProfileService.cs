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
using WebApp.Models;


namespace WebApp.Service;

public class ProfileService : IProfileService
{
    private readonly DataContext _db;
    private readonly IUserService _userService;
    private readonly IMessageService _messageService;
    private readonly IMapper _mapper;

    public ProfileService(IUserService userService, IMessageService messageService, IMapper mapper, DataContext db)
    {
        _db = db;
        _userService = userService;
        _messageService = messageService;
        _mapper = mapper;
    }
    public async Task<List<IProfile>> ListProfile(GetProfilesListQuery query, int itemsPerPage, int page)
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

        return await Task.FromResult(new List<IProfile>(users.Select(u => new UserProfile { Id = u.Id , User=u})));
    }

    public async Task<IProfile> GetProfile(Guid id)
    {
        return await Task.FromResult<IProfile>(new UserProfile()
        {
            Id = id
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
        return  await _userService.Add(new UserDto_WithLoginPassword()
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
        UserDto user = await _userService.Get(id);
        ICollection<MessageDto> messages = await _messageService.GetPagedAsync(1, 10);
        var profile = new PersonalProfile();
        profile.Id = id;
        profile.User = user;
        profile.Messages = messages;
        return profile;
    }
}