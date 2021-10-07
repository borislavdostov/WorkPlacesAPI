using Microsoft.EntityFrameworkCore;
using System;
using Workplaces.Data.Entities;

namespace Workplaces.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Workplace> Workplaces { get; set; }

        public DbSet<UserWorkplace> UserWorkplaces { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Workplace>().HasData(
        //        new Workplace { Id = 1, Name = "Web Developer", CreatedAt = DateTime.Now },
        //        new Workplace { Id = 2, Name = "QA Specialist", CreatedAt = DateTime.Now },
        //        new Workplace { Id = 3, Name = "Mobile Developer", CreatedAt = DateTime.Now },
        //        new Workplace { Id = 4, Name = "Full Stack Developer", CreatedAt = DateTime.Now });
        //}
    }
}
