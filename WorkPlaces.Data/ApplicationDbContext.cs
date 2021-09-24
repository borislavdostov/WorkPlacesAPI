using Microsoft.EntityFrameworkCore;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Workplace> WorkPlaces { get; set; }

        public DbSet<UserWorkplace> UserWorkPlaces { get; set; }
    }
}
