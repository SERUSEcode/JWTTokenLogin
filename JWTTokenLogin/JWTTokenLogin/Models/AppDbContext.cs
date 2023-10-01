using Microsoft.EntityFrameworkCore;

namespace JWTTokenLogin.Models
{
    public class AppDbContext : DbContext
    {
        public IConfiguration _config { get; set; }
        public AppDbContext(IConfiguration config)
        {
            this.Database.EnsureCreated();
            _config = config;
        }

        public AppDbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));
        //}

        public DbSet<User.UserModel> Users { get; set; }
        
    }
}
