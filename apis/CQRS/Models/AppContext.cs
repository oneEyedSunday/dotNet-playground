using Microsoft.EntityFrameworkCore;
using System;

namespace CQRSDemo.Models
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions options): base (options)
        {}

        public DbSet<Person> People { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Command> CommandStore { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasKey(p => p.NIN);

            modelBuilder.Entity<Command>()
                .HasKey(c => c.Id);
        }

    }
}

