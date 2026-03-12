namespace MermaidDotNet.EntityFrameworkCore.Tests.Mock.Entities
{
    internal class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public DateTime EnrolledAt { get; set; }
        public decimal? Grade { get; set; }
    }
}