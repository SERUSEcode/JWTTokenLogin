namespace JWTTokenLogin.Models.User
{
    public interface IUserRepository
    {
        public IEnumerable<UserModel> AllUsers { get; }

        UserModel CheckUser(string Username, string Password);
    }
}
