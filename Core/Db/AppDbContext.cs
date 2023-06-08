using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Core.Db
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<ApplicationUser>? ApplicationUser { get; set; }
        public DbSet<CommonDropdowns>? CommonDropdowns { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<UserVerification>? UserVerifications { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Location>? locations { get; set; }
        public DbSet<Shift>? shifts { get; set; }
        public DbSet<Activity>? activities { get; set; }
        public DbSet<Time>? Times { get; set; }
        public DbSet<SetupRota>? SetupRotas { get; set; }
    }
}

