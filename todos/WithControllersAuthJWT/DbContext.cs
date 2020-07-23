using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace WithControllersAuthJWT
{
    public class DbContext : IdentityDbContext<User>
    {
        public DbContext(DbContextOptions<DbContext> options): base(options)
        { }

       public DbSet<Todo> Todos { get; set; }
    }
}
