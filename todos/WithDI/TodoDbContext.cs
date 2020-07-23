using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WithDI
{

    public class Todo
    {
        public int Id { get; set; }
        // validation wont be applied automatically
        // No .AddControllers
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
    public class TodoDbContext: DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options): base(options)
        {
            // Todos.AddRange(new List<Todo> {
            //     new Todo { Name = "Serve God" },
            //     new Todo { Name = "Get Money" },
            //     new Todo { Name = "Lift parents" }
            // });

            // SaveChanges();
        }
        public DbSet<Todo> Todos { get; set; }
    }
}