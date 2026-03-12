namespace MermaidDotNet.EntityFrameworkCore.Tests.Mock.Entities
{
    internal class Teacher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; } = [];
        public ICollection<SchoolClass> SchoolClasses { get; set; } = [];
    }
}