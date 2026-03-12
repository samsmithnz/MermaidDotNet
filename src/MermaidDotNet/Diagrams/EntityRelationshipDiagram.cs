using MermaidDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidDotNet.Diagrams
{
    public class EntityRelationshipDiagram : ADiagram
    {
        public override string Name => "erDiagram";

        public EntityRelationshipDiagram(List<EntityRelationNode> nodes, List<EntityRelationLink> links) 
            : base(nodes.Cast<Node>().ToList(), links.Cast<Link>().ToList())
        {
        }

    }
}
