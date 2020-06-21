using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BasicWithAuth
{
    public class TodoDbContext : IdentityDbContext<BasicWithAuthUser>
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options): base (options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
    }
}
