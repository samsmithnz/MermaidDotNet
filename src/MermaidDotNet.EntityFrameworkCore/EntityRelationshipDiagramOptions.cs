using MermaidDotNet.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidDotNet.EntityFrameworkCore
{
    public class EntityRelationshipDiagramOptions
    {
        public ColumnKeyType FilterColumnByKeyTypes { get; set; } = ColumnKeyType.None;

        public bool IncludeColumns { get; set; } = true;
        public bool IncludeColumnKeyTypes { get; set; } = true;
        public bool IncludeColumnComments { get; set; } = true;
        public bool IncludeLinks { get; set; } = true;
        public bool IncludeLinkLabels { get; set; } = true;
        public bool IncludeLinkDeleteBehaviors { get; set; } = true;
    }
}
