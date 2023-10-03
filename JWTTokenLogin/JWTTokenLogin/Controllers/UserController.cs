using JWTTokenLogin.Models.User;
using JWTTokenLogin.Models;
using Microsoft.AspNetCore.Mvc;

namespace JWTTokenLogin.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _UserRepository;

        public UserController(IUserRepository userRepository)
        {
            this._UserRepository = userRepository;
        }

        [HttpGet]
        public IEnumerable<UserModel> GetUsers()
        {
            var users = _UserRepository.AllUsers;
            return users;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(string username, string password)
        {
            var users = new UserModel()
            {
                Username = username,
                Password = password,
                UserMessage = "",
                UserToken = ""
            };

            using (var db = new AppDbContext())
            {
                db.Add(users);
                db.SaveChanges();
            }

            return Ok(users);
        }
    }
}
