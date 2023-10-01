namespace JWTTokenLogin.Models.User
{
    public class DbUserRepository : IUserRepository
    {
        private AppDbContext _appDbContext;

        public DbUserRepository(AppDbContext AppDbContext)
        {
            _appDbContext = AppDbContext;
        }

        public IEnumerable<UserModel> AllUsers => _appDbContext.Users;

        public UserModel CheckUser(string username, string password)
        {
            return _appDbContext.Users.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
        }
    }
}
