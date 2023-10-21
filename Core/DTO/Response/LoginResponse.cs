namespace Core.DTO.Response
{
    public class LoginResponse : DefaultResponse
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public int? UserId { get; set; }
    }
}
