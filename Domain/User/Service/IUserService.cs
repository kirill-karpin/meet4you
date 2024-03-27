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
        public Task<UserDto> Update(UserDto userDto);
        public Task<string> GetHashPasswordWithSalt(string unhashedPassword, string salt);
        public Task<List<UserDto>> GetPagedAsync(UserFilterDto userFilterDto);
        public Task<bool> AddRequestToChangePassword(Guid userId, string password);
        public Task<string> GetConfirmationCode(Guid userId);
        public Task<bool> ChangePassword(Guid userId, string newPassword, string confirmationCode);
        public Task<List<UserDto>> GetPagedAsync(UserFilterDto userFilterDto);
    }
}