using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ISP_API.Data.UserDTOs;
using ISP_API.Dtos;
using ISP_API.Entities;
using ISP_API.Services.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ISP_API.Services;

public class UserService : IUserService
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly IConfiguration _configuration;
    private readonly JwtSettings _jwtSettings;
    private readonly ILogger<UserService> _logger;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<UserEntity> _signInManager;
    public UserService(UserManager<UserEntity> userManager, 
        IConfiguration configuration, 
        IOptions<JwtSettings> jwtSettings, 
        ILogger<UserService> logger,
        RoleManager<IdentityRole> roleManager,
        SignInManager<UserEntity> signInManager)
    {
        _userManager = userManager;
        _configuration = configuration;
        _jwtSettings = jwtSettings.Value;
        _logger = logger;
        _roleManager = roleManager;
        _signInManager = signInManager;
    }
    //create user
    public async Task<ResponseDto<IdentityResult>> RegisterUserASync(CreateUserDto userDto)
    {
        var user = new UserEntity
        {
            UserName = userDto.Username,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName
        };
        
        var result = await _userManager.CreateAsync(user, userDto.Password);
        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole {Name = "User"});
            }
            await _userManager.AddToRoleAsync(user, "User");
            return new ResponseDto<IdentityResult>
            {
                StatusCode = 201,
                Status = true,
                Message = "User registered successfully",
                Data = result
            };
        }

        return new ResponseDto<IdentityResult>
        {
            StatusCode = 400,
            Status = false,
            Message = "User registration failed" + string.Join(", ", result.Errors.Select(e => e.Description)),
            Data = result
        };
    }
    
    //login user
    public async Task<ResponseDto<LoginResponseDto>> LoginUserAsync(LoginDto dto)
    {
        var result = await _signInManager.PasswordSignInAsync(
            dto.Username,
            dto.Password,
            isPersistent: false,
            lockoutOnFailure: false
        );

        if (result.Succeeded)
        {
            var userEntity = await _userManager.FindByNameAsync(dto.Username);
            
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userEntity.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("UserId", userEntity.Id)
            };
            var userRoles = await _userManager.GetRolesAsync(userEntity);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            var token = GenerateJwtTokenAsync(authClaims);
            return new ResponseDto<LoginResponseDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Login successful",
                Data = new LoginResponseDto
                {
                    Username = userEntity.UserName,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    TokenExpiration = token.ValidTo
                }
            };
        }
        return new ResponseDto<LoginResponseDto>
        {
            StatusCode = 401,
            Status = false,
            Message = "Invalid username or password",
        };        

    }

    private JwtSecurityToken GenerateJwtTokenAsync(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
        
        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(12),
            claims: authClaims,
            signingCredentials: new SigningCredentials(
                authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }
    
}

