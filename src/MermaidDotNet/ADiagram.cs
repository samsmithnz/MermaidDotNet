using MermaidDotNet.Constants;
using MermaidDotNet.Enums;
using MermaidDotNet.Extensions;
using MermaidDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidDotNet
{
    public abstract class ADiagram
    {
        public abstract string Name { get; }
        public List<Node> Nodes { get; set; }
        public List<Link> Links { get; set; }
        public List<string> LinkStyles { get; set; }
        public List<string> NodeClasses { get; set; }

        /// <summary>
        /// Initialize the flowchart
        /// </summary>
        /// <param name="direction">Accepts LR, TD, BT, RL, and TB options</param>
        /// <param name="nodes">A list of nodes</param>
        /// <param name="links">A list of links</param>
        public ADiagram(List<Node> nodes, List<Link> links)
        {
            Nodes = nodes;
            Links = links;
            LinkStyles = new();
            NodeClasses = new();
        }

        /// <summary>
        /// Given a list of nodes and links, calculate the mermaid flowchart
        /// </summary>
        /// <returns>a mermaid graph as a string</returns>
        public virtual string CalculateDiagram()
        {
            var lines = new List<string>();
            lines.Add(Name);

            lines.AddRange(Nodes.Select(n => n.GetNodeString()).ClearNewLines().Indent());
            lines.AddRange(Links.Select(n => n.GetLinkString()).ClearNewLines().Indent());
            var linkStyles = Links.Where(l => !string.IsNullOrEmpty(l.LinkStyle)).ToList();
            lines.AddRange(linkStyles.Select(n => n.GetStyleString(linkStyles.IndexOf(n))).ClearNewLines().Indent());
            lines.AddRange(Nodes.Select(n => n.GetClassString()).ClearNewLines().Indent());

            return string.Join(Environment.NewLine, lines);
        }
    }
}
