using System.Linq;
using System.Threading.Tasks;
using Nike.Application.Common.Exceptions;
using Nike.Application.Common.Interfaces;
using Nike.Application.Common.Models;
using Nike.Application.Dto;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using Mapster;
using System.Collections.Generic;

namespace Nike.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                throw new UnauthorizeException();
            }

            return user.UserName;
        }

        public async Task<ApplicationUserDto> CheckUserPassword(string userName, string password)
        {
            ApplicationUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user != null && await _userManager.CheckPasswordAsync(user, password))
            {
                return _mapper.Map<ApplicationUserDto>(user);
            }

            return null;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> UserIsInRole(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<ApplicationUserDto> GetUserByToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            SecurityToken securityToken;
            var principle = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken != null && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                var userId = principle.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
                var userRoles = await _userManager.GetRolesAsync(user);

                var UserDto = await _userManager.Users.Include(u => u.Roles)
                    .Where(u => u.Id == userId)
                    .ProjectToType<ApplicationUserDto>(_mapper.Config)
                    .FirstOrDefaultAsync();

                foreach (var userRole in userRoles)
                {
                    var role = await _roleManager.FindByNameAsync(userRole);
                    UserDto.Roles.Add(role);
                }

                return UserDto;
            }

            return null;
        }

        public async Task<Result> AddUserToRole(string userId, string roleId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var role = await _roleManager.FindByIdAsync(roleId);
            var result = await _userManager.AddToRolesAsync(user, new[] { role.Name });
            return result.ToApplicationResult();
        }

        public async  Task<List<ApplicationUserDto>> GetAllUsers()
        {
            var userList = _userManager.Users.Include(u => u.Roles);
            var userListDto = await userList
                .ProjectToType<ApplicationUserDto>(_mapper.Config)
                .ToListAsync();
            
            foreach (var userDto in userListDto)
            {
                var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userDto.Id);
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach (var userRole in userRoles)
                {
                    var role = await _roleManager.FindByNameAsync(userRole);
                    userDto.Roles.Add(role);
                }

            }

            return userListDto;
        }
    }
}
