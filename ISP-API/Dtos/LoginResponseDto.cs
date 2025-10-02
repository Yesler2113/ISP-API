namespace ISP_API.Dtos
{
    public class LoginResponseDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime TokenExpiration { get; set; }
    }
}