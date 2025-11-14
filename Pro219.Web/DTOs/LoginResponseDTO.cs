namespace Pro219.Web.DTOs
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool LoginSuccess { get; set; }
    }
}

