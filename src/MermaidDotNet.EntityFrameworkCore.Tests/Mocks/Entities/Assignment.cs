namespace MermaidDotNet.EntityFrameworkCore.Tests.Mock.Entities
{
    internal class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateOnly DueDate { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<Submission> Submissions { get; set; } = [];
    }
}