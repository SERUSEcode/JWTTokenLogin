using Microsoft.EntityFrameworkCore;

namespace JWTTokenLogin.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<User.UserModel> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=JWTTokenLoginDb;Trusted_Connection=True");
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        
        }

        

        public AppDbContext()
        {
            this.Database.EnsureCreated();
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(_config.GetConnectionString("DatabaseConnection"));
        //}

        
        
    }
}
