using Microsoft.EntityFrameworkCore.Metadata;

namespace MermaidDotNet.EntityFrameworkCore.Models
{
    internal class DiagramTable
    {
        public IEntityType EntityType { get; set; }
        public string Name { get; set; }
        public List<DiagramColumn> Columns { get; set; } = new List<DiagramColumn>();
    }
}
