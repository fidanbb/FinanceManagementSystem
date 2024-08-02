using FinanceManagementSystem.Application.Dtos.User;
using FinanceManagementSystem.Application.Helpers.Enums;
using FinanceManagementSystem.Application.Helpers.Responses;
using FinanceManagementSystem.Application.Services.Token;
using FinanceManagementSystem.Application.Services.User;
using FinanceManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Persistence.Services.User
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenHandler;

        public UserService(ITokenService tokenHandler,
                          UserManager<AppUser> userManager,
                          RoleManager<IdentityRole> roleManager)
        {
            _tokenHandler = tokenHandler;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<BaseResponse> AddRoleToUserAsync(UserRoleDto request)
        {
            AppUser user = await _userManager.FindByIdAsync(request.UserId) ?? throw new NullReferenceException();
            IdentityRole role = await _roleManager.FindByIdAsync(request.RoleId) ?? throw new NullReferenceException();
            IList<string> userRoles = await _userManager.GetRolesAsync(user);

            if (userRoles.Any(m => m == role.Name))
            {
                return new BaseResponse { IsSuccess = false, ErrorMessage = $"{role.Name} already exist" };
            }

            await _userManager.AddToRoleAsync(user, role.Name);

            return new BaseResponse { IsSuccess = true, ErrorMessage = null };
        }

        public async Task CreateRoleAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }

        public List<string> GetAllRoles()
        {
            return _roleManager.Roles.Select(m => m.Name).ToList();
        }

        public List<UserDto> GetAllUsers()
        {
            var users = _userManager.Users;

            return users.Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Surname = x.Surname,
                Username = x.UserName,
                Email = x.Email
            }).ToList();
        }

        public async Task<LoginResponse> SignInAsync(LoginDto request)
        {
            ArgumentNullException.ThrowIfNull(nameof(request));

            AppUser existUser = await _userManager.FindByEmailAsync(request.Email);

            if (existUser == null) return new LoginResponse { IsSuccess = false, Token = null, Errors = new List<string> { "Email or Password is wrong" } };

            if (!await _userManager.CheckPasswordAsync(existUser, request.Password))
            {
                return new LoginResponse { IsSuccess = false, Token = null, Errors = new List<string> { "Email or Password is wrong" } };
            }
            var userRoles = await _userManager.GetRolesAsync(existUser);

            CheckAppUserDto dto = new()
            {
                Id = existUser.Id,
                Username = existUser.UserName,
                Roles = userRoles.ToList(),
                Name = existUser.Name,
                Surname = existUser.Surname,
            };

            JwtTokenResponse response = _tokenHandler.GenerateJwtToken(dto);


            return new LoginResponse { IsSuccess = true, Token = response.Token, ExpireDate = response.ExpireDate, Errors = null };
        }

        public async Task<RegisterResponse> SignUpAsync(RegisterDto request)
        {
            ArgumentNullException.ThrowIfNull(request, "Paremetr not found");

            AppUser user = new()
            {
                UserName = request.Username,
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                PasswordHash = request.Password
            };

            IdentityResult response = await _userManager.CreateAsync(user, request.Password);

            if (!response.Succeeded)
            {
                return new RegisterResponse
                {
                    isSuccess = false,
                    Errors = response.Errors.Select(m => m.Description).ToList()
                };
            }

            await _userManager.AddToRoleAsync(user, Roles.Member.ToString());

            return new RegisterResponse { isSuccess = true, Errors = null };
        }

    }
}
