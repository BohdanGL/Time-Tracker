using Microsoft.EntityFrameworkCore;
using TimeTracker.Models;

namespace TimeTracker.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<ActivityType> ActivityTypes { get; set; } = null!;
        public DbSet<Tracker> Trackers { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
