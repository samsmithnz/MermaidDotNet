using MermaidDotNet.Enums;

namespace MermaidDotNet.Models
{
    public class EntityRelationColumn
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ColumnKeyType ColumnKeyType { get; set; }
        public string Comment { get; set; }
        public EntityRelationColumn(string name, string type = "", ColumnKeyType columnKeyType = ColumnKeyType.None, string comment = "")
        {
            Name = name;
            Type = type;
            ColumnKeyType = columnKeyType;
            Comment = comment;
        }

        public string GetColumnString()
        {
            var str = Type;

            if (!string.IsNullOrEmpty(Name))
            {
                if (string.IsNullOrEmpty(str))
                {
                    str = Name;
                }
                else
                {
                    str = string.Join(" ", str, Name);
                }
            }

            if (ColumnKeyType != ColumnKeyType.None)
            {
                str = string.Join(" ", str, GetRelationReferenceString());
            }

            if (!string.IsNullOrEmpty(Comment))
            {
                str = string.Join(" ", str, $"\"{Comment}\"");
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
                references.Add("UK");

            return references.Count > 0 ? string.Join(", ", references) : string.Empty;
        }
    }
}
