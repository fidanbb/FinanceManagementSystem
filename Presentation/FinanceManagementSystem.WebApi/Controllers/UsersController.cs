using FinanceManagementSystem.Application.Dtos.User;
using FinanceManagementSystem.Application.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceManagementSystem.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _UserService;
        public UsersController(IUserService UserService)
        {
            _UserService = UserService;
        }


        [HttpGet]
        public async Task<IActionResult> CreateRole()
        {
            await _UserService.CreateRoleAsync();
            return Ok();
        }



        [HttpGet]

        public IActionResult GetRoles()
        {
            return Ok(_UserService.GetAllRoles());
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp([FromBody] RegisterDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _UserService.SignUpAsync(request));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] LoginDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _UserService.SignInAsync(request));
        }


        [HttpGet]

        public IActionResult GetAllUsers()
        {
            return Ok(_UserService.GetAllUsers());
        }

        [HttpPost]

        public async Task<IActionResult> AddRoleToUser([FromBody] UserRoleDto request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            return Ok(await _UserService.AddRoleToUserAsync(request));
        }
    }
}
