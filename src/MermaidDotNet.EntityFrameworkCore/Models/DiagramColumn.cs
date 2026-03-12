using MermaidDotNet.Enums;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MermaidDotNet.EntityFrameworkCore.Models
{
    internal class DiagramColumn
    {
        public IProperty Property { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public bool IsNullable { get; set; }
        public ColumnKeyType ColumnKeyType { get; set; }
    }
}
