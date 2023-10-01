namespace JWTTokenLogin.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserMessage { get; set; }
        public string UserToken { get; set; }
    }
}
