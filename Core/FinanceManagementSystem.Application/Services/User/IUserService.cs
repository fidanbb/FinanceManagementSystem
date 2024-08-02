using FinanceManagementSystem.Application.Dtos.User;
using FinanceManagementSystem.Application.Helpers.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManagementSystem.Application.Services.User
{
    public interface IUserService
    {
        Task CreateRoleAsync();
        List<string> GetAllRoles();
        Task<RegisterResponse> SignUpAsync(RegisterDto request);
        List<UserDto> GetAllUsers();
        Task<LoginResponse> SignInAsync(LoginDto request);

        Task<BaseResponse> AddRoleToUserAsync(UserRoleDto request);
    }
}
