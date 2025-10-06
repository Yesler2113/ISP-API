using ISP_API.Data.UserDTOs;
using ISP_API.Dtos;
using Microsoft.AspNetCore.Identity;

namespace ISP_API.Services.Entities;

public interface IUserService
{
    Task<ResponseDto<IdentityResult>> RegisterUserASync(CreateUserDto userDto);
    Task<ResponseDto<LoginResponseDto>> LoginUserAsync(LoginDto dto);
    
}