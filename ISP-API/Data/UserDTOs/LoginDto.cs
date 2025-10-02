using System.ComponentModel.DataAnnotations;

namespace ISP_API.Data.UserDTOs;

public class LoginDto
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}