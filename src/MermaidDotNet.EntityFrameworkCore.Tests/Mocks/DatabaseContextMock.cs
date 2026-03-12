using MermaidDotNet.EntityFrameworkCore.Tests.Mock.Entities;
using Microsoft.EntityFrameworkCore;

namespace MermaidDotNet.EntityFrameworkCore.Tests.Mock
{
    internal class DatabaseContextMock : DbContext
    {
        public DatabaseContextMock(DbContextOptions<DatabaseContextMock> options) : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<SchoolClass> SchoolClasses => Set<SchoolClass>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<Submission> Submissions => Set<Submission>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.OwnsOne(s => s.Address);
                entity.HasOne(s => s.SchoolClass)
                      .WithMany(c => c.Students)
                      .HasForeignKey(s => s.SchoolClassId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(t => t.Id);
            });

            modelBuilder.Entity<SchoolClass>(entity =>
            {
                entity.HasKey(sc => sc.Id);
                entity.HasOne(sc => sc.Teacher)
                      .WithMany(t => t.SchoolClasses)
                      .HasForeignKey(sc => sc.TeacherId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.Teacher)
                      .WithMany(t => t.Courses)
                      .HasForeignKey(c => c.TeacherId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Student)
                      .WithMany(s => s.Enrollments)
                      .HasForeignKey(e => e.StudentId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Course)
                      .WithMany(c => c.Enrollments)
                      .HasForeignKey(e => e.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Assignment>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasOne(a => a.Course)
                      .WithMany(c => c.Assignments)
                      .HasForeignKey(a => a.CourseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Submission>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.HasOne(s => s.Assignment)
                      .WithMany(a => a.Submissions)
                      .HasForeignKey(s => s.AssignmentId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(s => s.Student)
                      .WithMany(st => st.Submissions)
                      .HasForeignKey(s => s.StudentId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}