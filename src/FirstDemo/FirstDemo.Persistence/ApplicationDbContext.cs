using FirstDemo.Domain.Entities.Training;
using FirstDemo.Persistence.Features.Membership;
using FirstDemo.Persistence.Seeds;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FirstDemo.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, 
        ApplicationRole, Guid,
        ApplicationUserClaim, ApplicationUserRole, 
        ApplicationUserLogin, ApplicationRoleClaim, 
        ApplicationUserToken>, 
        IApplicationDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public ApplicationDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_connectionString,
					x => x.MigrationsAssembly(_migrationAssembly));
			}

            base.OnConfiguring(optionsBuilder);
        }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.Entity<Student>().HasData(StudentSeed.Students);

            modelBuilder.Entity<CourseStudent>()
                .HasKey((x) => new { x.CourseId, x.StudentId });

            modelBuilder.Entity<CourseStudent>()
                .HasOne(x => x.Course)
                .WithMany(y => y.Students)
                .HasForeignKey(z => z.CourseId);

            modelBuilder.Entity<CourseStudent>()
                .HasOne(x => x.Student)
                .WithMany(y => y.Courses)
                .HasForeignKey(z => z.StudentId);

            base.OnModelCreating(modelBuilder);
        }

		public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}