using Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace Core.Db
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<UserVerification> UserVerifications { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Location> locations { get; set; }
        public DbSet<Shifts> shift { get; set; }
        public DbSet<StaffRota> StaffRotas { get; set; }
		public DbSet<Attendance> Attendances { get; set; }
		public DbSet<Country> Country { get; set; }
		public DbSet<StaffClockIn> StaffClockIns { get; set; }
		public DbSet<State> State { get; set; }
		public DbSet<ShiftsLocation> ShiftsLocations { get; set; }
        public DbSet<LeaveApplication> LeaveApplications { get; set; }
        public DbSet<LeaveSetup> LeaveSetups { get; set; }
    }
}

