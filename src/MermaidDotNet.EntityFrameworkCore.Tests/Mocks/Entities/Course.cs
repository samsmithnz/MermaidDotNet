namespace MermaidDotNet.EntityFrameworkCore.Tests.Mock.Entities
{
    internal class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = [];
        public ICollection<Assignment> Assignments { get; set; } = [];
    }
}