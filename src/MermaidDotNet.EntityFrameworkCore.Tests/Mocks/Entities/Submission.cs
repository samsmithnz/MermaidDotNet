using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace MermaidDotNet.EntityFrameworkCore.Tests.Mock.Entities
{
    internal class Submission
    {
        public int Id { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public DateTime SubmittedAt { get; set; }
        [Description("The grade awarded for the submission, if graded.")]
        public decimal? Grade { get; set; }
        public string? Comment { get; set; }
    }
}