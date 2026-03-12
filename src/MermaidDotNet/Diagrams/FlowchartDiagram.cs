using MermaidDotNet.Constants;
using MermaidDotNet.Extensions;
using MermaidDotNet.Models;
using System.Text;

namespace MermaidDotNet.Diagrams;

public class FlowchartDiagram : ADiagram
{
    public override string Name => "flowchart";
    public string Direction { get; set; }
    public List<SubGraph> SubGraphs { get; set; }
    public List<FlowNode> AllNodes { get; set; }
    public List<Link> AllLinks { get; set; }

    /// <summary>
    /// Initialize the flowchart
    /// </summary>
    /// <param name="direction">Accepts LR, TD, BT, RL, and TB options</param>
    /// <param name="nodes">A list of nodes</param>
    /// <param name="links">A list of links</param>
    public FlowchartDiagram(List<FlowNode> nodes, List<Link> links, string direction, List<SubGraph>? subGraphs = null)
            : base(nodes.Cast<Node>().ToList(), links)
    {
        if (direction != "LR" && direction != "TD" && direction != "BT" && direction != "RL" && direction != "TB")
        {
            throw new NotSupportedException("Direction " + direction + " is currently unsupported");
        }

        Direction = direction;
        SubGraphs = subGraphs ?? new();
        AllNodes = new();
        AllNodes.AddRange(Nodes.Cast<FlowNode>());
        AllNodes.AddRange(SubGraphs.SelectMany(sg => sg.Nodes));
        AllLinks = new();
        AllLinks.AddRange(Links);
        AllLinks.AddRange(SubGraphs.SelectMany(sg => sg.Links));
    }

    /// <summary>
    /// Given a list of nodes and links, calculate the mermaid flowchart
    /// </summary>
    /// <returns>a mermaid graph as a string</returns>
    public override string CalculateDiagram()
    {
        var lines = new List<string>();
        lines.Add($"{Name} {Direction}");

        //Add Subgroups, and their nodes and links
        foreach (SubGraph subGroup in SubGraphs)
        {
            lines.Add($"{FormattingConstants.Indentation}subgraph {subGroup.Name}");

            if (subGroup.Direction != null)
            {
                lines.Add($"{FormattingConstants.Indentation}direction {subGroup.Direction}");
            }

            lines.AddRange(subGroup.Nodes.Select(n => n.GetNodeString()).ClearNewLines().Indent());
            lines.AddRange(subGroup.Links.Select(n => n.GetLinkString()).ClearNewLines().Indent());
            lines.Add($"{FormattingConstants.Indentation}end");
        }

        lines.AddRange(Nodes.Select(n => n.GetNodeString()).ClearNewLines().Indent());
        lines.AddRange(Links.Select(n => n.GetLinkString()).ClearNewLines().Indent());
        var linkStyles = AllLinks.Where(l => !string.IsNullOrEmpty(l.LinkStyle)).ToList();
        lines.AddRange(linkStyles.Select(n => n.GetStyleString(linkStyles.IndexOf(n))).ClearNewLines().Indent());
        lines.AddRange(AllNodes.Select(n => n.GetClassString()).ClearNewLines().Indent());
        lines.AddRange(AllNodes.Select(n => n.GetClickActionString()).ClearNewLines().Indent());

        return string.Join(Environment.NewLine, lines);
    }
}
