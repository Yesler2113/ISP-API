using ISP_API.Data.UserDTOs;
using ISP_API.Dtos;
using ISP_API.Entities;
using ISP_API.Services.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ISP_API.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly IUserService _userService;
    public AccountController(IConfiguration configuration, IUserService userService, UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager)
    {
        _configuration = configuration;
        _userManager = userManager;
        _signInManager = signInManager;
        _userService = userService;
        
    }


    [HttpPost("Register")]
    public async Task<ActionResult<ResponseDto<CreateUserDto>>> Register([FromBody] CreateUserDto model)
    {
        if(ModelState.IsValid)
        {
            var response = await _userService.RegisterUserASync(model);
            if (response.Status)
            {
                return Ok(new {Message = response.Message});
            }
            return BadRequest(new {Message = response.Message});
        }
        return BadRequest(ModelState);
    }

    [HttpPost("Login")]
    public async Task<ActionResult<ResponseDto<LoginResponseDto>>> Login([FromBody] LoginDto model)
    {
        var authResponse = await _userService.LoginUserAsync(model);
        
        return StatusCode(authResponse.Status ? 200 : 500, authResponse);
    }
}