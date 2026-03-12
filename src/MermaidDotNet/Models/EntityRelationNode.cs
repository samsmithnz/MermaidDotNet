using MermaidDotNet.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidDotNet.Models
{
    public class EntityRelationNode : Node
    {
        public List<EntityRelationColumn> Columns { get; set; }
        public EntityRelationNode(string name, List<EntityRelationColumn> columns, string? cssClass = null)
            : this(name, "", columns, cssClass)
        {

        }
        public EntityRelationNode(string name, string text, List<EntityRelationColumn> columns, string? cssClass = null)
            : base(name, text, cssClass)
        {
            Columns = columns;
        }


        public override string GetNodeString()
        {
            var lines = new List<string>();
            lines.Add(string.Join(" ", base.GetNodeString(), "{"));
            lines.AddRange(Columns.Select(c => c.GetColumnString()).Indent());
            lines.Add("}");
            return string.Join(Environment.NewLine, lines);
        }
    }
}
