using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample
{
	public class TrainingDbContext : DbContext
	{
		private readonly string _connectionString;

		public TrainingDbContext()
		{
			_connectionString = "Server=.\\SQLEXPRESS;Database=Aspnetb8;User Id=aspnetb8;Password=123456;Trust Server Certificate=True;";
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if(!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(_connectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Topic>().ToTable("Topics");
			modelBuilder.Entity<CourseStudent>().HasKey((x) => new { x.CourseId, x.StudentId });

			modelBuilder.Entity<Course>()
				.HasMany(x => x.Topics)
				.WithOne(x => x.Course)
				.HasForeignKey(x => x.CourseId);

			modelBuilder.Entity<CourseStudent>()
			   .HasOne(x => x.Course)
			   .WithMany(x => x.Students)
			   .HasForeignKey(x => x.CourseId);

			modelBuilder.Entity<CourseStudent>()
			   .HasOne(x => x.Student)
			   .WithMany(x => x.Courses)
			   .HasForeignKey(x => x.StudentId);
		}

		public DbSet<Course> Courses { get; set; }
		public DbSet<Student> Students { get; set; }
	}
}
