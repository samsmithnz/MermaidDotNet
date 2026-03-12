using MermaidDotNet.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidDotNet.Models
{
    public class EntityRelationColumn
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ColumnKeyType ColumnKeyType { get; set; }
        public EntityRelationColumn(string name, string type = "", ColumnKeyType columnKeyType = ColumnKeyType.None)
        {
            Name = name;
            Type = type;
            ColumnKeyType = columnKeyType;
        }

        public string GetColumnString()
        {
            var str = Name;

            if (!string.IsNullOrEmpty(Type))
            {
                str = string.Join(" ", str, Type);
            }

            if (ColumnKeyType != ColumnKeyType.None)
            {
                str = string.Join(" ", str, $"\"{GetRelationReferenceString()}\"");
            }

            return str;
        }

        private string GetRelationReferenceString()
        {
            var references = new List<string>();

            if ((ColumnKeyType & ColumnKeyType.PrimaryKey) == ColumnKeyType.PrimaryKey)
                references.Add("PK");
            if ((ColumnKeyType & ColumnKeyType.ForeignKey) == ColumnKeyType.ForeignKey)
                references.Add("FK");
            if ((ColumnKeyType & ColumnKeyType.UniqueKey) == ColumnKeyType.UniqueKey)
                references.Add("Unique");
            if ((ColumnKeyType & ColumnKeyType.Indexed) == ColumnKeyType.Indexed)
                references.Add("Indexed");

            return references.Count > 0 ? string.Join(", ", references) : string.Empty;
        }
    }
}
