using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nike.Application.Common.Models;
using Nike.Application.Dto;

namespace Nike.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<ApplicationUserDto> CheckUserPassword(string userName, string password);

        Task<ApplicationUserDto> GetUserByToken(string token);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

        Task<Result> AddUserToRole(string userId, string roleId);

        Task<bool> UserIsInRole(string userId, string role);

        Task<Result> DeleteUserAsync(string userId);
        Task<List<ApplicationUserDto>> GetAllUsers();
    }
}
