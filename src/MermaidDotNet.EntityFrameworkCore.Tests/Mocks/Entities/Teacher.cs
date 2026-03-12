using DescriptionAttribute = System.ComponentModel.DescriptionAttribute;

namespace MermaidDotNet.EntityFrameworkCore.Tests.Mock.Entities
{
    internal class Teacher
    {
        [Description("The unique identifier for the teacher.")]
        public int Id { get; set; }
        [Description("The first name of the teacher.")]
        public string FirstName { get; set; }
        [Description("The last name of the teacher.")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Course> Courses { get; set; } = [];
        public ICollection<SchoolClass> SchoolClasses { get; set; } = [];
    }
}