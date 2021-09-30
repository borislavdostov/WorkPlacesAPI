using Microsoft.EntityFrameworkCore;
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
    }
}
