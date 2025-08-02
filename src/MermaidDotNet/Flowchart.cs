using MermaidDotNet.Models;
using System.Text;

namespace MermaidDotNet;

public class Flowchart
{
    public string Direction { get; set; }
    public List<Node> Nodes { get; set; }
    public List<Link> Links { get; set; }
    public List<string> LinkStyles { get; set; }
    public List<SubGraph>? SubGraphs { get; set; }
    public List<Node> NavigationNodes { get; set; }
    public List<string> NodeClasses { get; set; }
    public List<string> ClickActions { get; set; }

    /// <summary>
    /// Initialize the flowchart
    /// </summary>
    /// <param name="direction">Accepts LR, TD, BT, RL, and TB options</param>
    /// <param name="nodes">A list of nodes</param>
    /// <param name="links">A list of links</param>
    public Flowchart(string direction, List<Node> nodes, List<Link> links, List<SubGraph>? subGraphs = null)
    {
        if (direction != "LR" && direction != "TD" && direction != "BT" && direction != "RL" && direction != "TB")
        {
            throw new NotSupportedException("Direction " + direction + " is currently unsupported");
        }
        else
        {
            Direction = direction;
        }
        Nodes = nodes;
        Links = links;
        LinkStyles = new();
        NodeClasses = new();
        ClickActions = new();
        SubGraphs = subGraphs;
        NavigationNodes = new();
        foreach (Node node in Nodes)
        {
            NavigationNodes.Add(node);
        }
        if (SubGraphs != null)
        {
            foreach (SubGraph subGraph in SubGraphs)
            {
                foreach (Node node in subGraph.Nodes)
                {
                    NavigationNodes.Add(node);
                }
            }
        }
    }

    /// <summary>
    /// Given a list of nodes and links, calculate the mermaid flowchart
    /// </summary>
    /// <returns>a mermaid graph as a string</returns>
    public string CalculateFlowchart()
    {
        StringBuilder sb = new();
        sb.Append("flowchart " + Direction + Environment.NewLine);

        //Add Subgroups, and their nodes and links
        if (SubGraphs != null)
        {
            foreach (SubGraph subGroup in SubGraphs)
            {
                sb.Append("    ");
                sb.Append("subgraph ");
                sb.Append(subGroup.Name);
                sb.Append(Environment.NewLine);
                if (subGroup.Direction != null)
                {
                    sb.Append("    ");
                    sb.Append("direction ");
                    sb.Append(subGroup.Direction);
                    sb.Append(Environment.NewLine);
                }
                foreach (Node node in subGroup.Nodes)
                {
                    sb.Append(AddNode(node));
                }
                foreach (Link link in subGroup.Links)
                {
                    sb.Append(AddLink(link));
                }
                sb.Append("    ");
                sb.Append("end");
                sb.Append(Environment.NewLine);
            }
        }

        //Add nodes
        foreach (Node node in Nodes)
        {
            sb.Append(AddNode(node));
        }

        //Add links
        foreach (Link link in Links)
        {
            sb.Append(AddLink(link));
        }

        //Add link styles
        if (LinkStyles.Count > 0)
        {
            for (int i = 0; i < LinkStyles.Count; i++)
            {
                sb.Append("    ");
                sb.Append("linkStyle ");
                sb.Append(i.ToString());
                sb.Append(' ');
                sb.Append(LinkStyles[i]);
                sb.Append(Environment.NewLine);
            }
        }

        //Add node classes
        if (NodeClasses.Count > 0)
        {
            foreach (string nodeClass in NodeClasses)
            {
                sb.Append("    ");
                sb.Append(nodeClass);
                sb.Append(Environment.NewLine);
            }
        }

        //Add click actions
        if (ClickActions.Count > 0)
        {
            foreach (string clickAction in ClickActions)
            {
                sb.Append("    ");
                sb.Append(clickAction);
                sb.Append(Environment.NewLine);
            }
        }

        return sb.ToString();
    }

    private string AddNode(Node node)
    {
        StringBuilder sb = new();
        sb.Append("    ");
        sb.Append(node.Name);
        sb.Append(node.OpenShape());
        sb.Append(node.Text);
        sb.Append(node.CloseShape());
        sb.Append(Environment.NewLine);

        // Track node styling and actions for later addition
        if (!string.IsNullOrEmpty(node.CssClass))
        {
            NodeClasses.Add($"class {node.Name} {node.CssClass}");
        }
        if (!string.IsNullOrEmpty(node.ClickAction))
        {
            ClickActions.Add($"click {node.Name} \"{node.ClickAction}\"");
        }

        return sb.ToString();
    }

    private string AddLink(Link link)
    {
        StringBuilder sb = new();
        if (NavigationNodes.Count == 0 && Nodes.Count > 1)
        {
            throw new ArgumentException("The NavigationNodes collection is empty, but Nodes collection is not empty. This is likely an issue because Nodes were added manually instead of as a collection in the FlowChart constructor");
        }
        Node? sourceNode = NavigationNodes.Find(n => n.Name == link.SourceNode);
        Node? destinationNode = NavigationNodes.Find(n => n.Name == link.DestinationNode);
        if (sourceNode == null)
        {
            throw new ArgumentException("Source node (" + link.SourceNode + ") in link connection (" + link.SourceNode + "-->" + link.DestinationNode + ") not found");
        }
        if (destinationNode == null)
        {
            throw new ArgumentException("Destination node (" + link.DestinationNode + ") in link connection (" + link.SourceNode + "-->" + link.DestinationNode + ") not found");
        }

        sb.Append("    ");
        sb.Append(sourceNode.Name);

        if (link.IsBidirectional)
        {
            sb.Append(GetStartArrowSymbol(link.Arrow));
        }

        sb.Append(GetLinkSymbol(link.Type));
        if (!string.IsNullOrEmpty(link.Text))
        {
            if (link.Type == Link.LinkType.Dotted)
            {
                sb.Append(link.Text);
                sb.Append(".-");
            }
            else
            {
                sb.Append(link.Text);
                sb.Append(GetLinkSymbol(link.Type));
            }
        }
        sb.Append(GetEndArrowSymbol(link.Arrow));
        sb.Append(destinationNode.Name);
        sb.Append(Environment.NewLine);

        //if there is a style, add it to the list
        if (!string.IsNullOrEmpty(link.LinkStyle))
        {
            LinkStyles.Add(link.LinkStyle);
        }

        return sb.ToString();
    }

    private string GetLinkSymbol(Link.LinkType linkType)
    {
        return linkType switch
        {
            Link.LinkType.Normal => "--",
            Link.LinkType.Dotted => "-.",
            Link.LinkType.Thick => "==",
            Link.LinkType.Invisible => "~~~",
            _ => "--"
        };
    }

    private string GetStartArrowSymbol(Link.ArrowType arrowType)
    {
        return arrowType switch
        {
            Link.ArrowType.Normal => "<",
            Link.ArrowType.Circle => "o",
            Link.ArrowType.Cross => "x",
            Link.ArrowType.Open => "<",
            _ => "<"
        };
    }

    private string GetEndArrowSymbol(Link.ArrowType arrowType)
    {
        return arrowType switch
        {
            Link.ArrowType.Normal => ">",
            Link.ArrowType.Circle => "o",
            Link.ArrowType.Cross => "x", 
            Link.ArrowType.Open => ">",
            _ => ">"
        };
    }

}
