using MermaidDotNet.Models;
using System.Text;
using System.Xml.Linq;

namespace MermaidDotNet;

public class Flowchart
{
    public string Direction { get; set; }
    public List<Node> Nodes { get; set; }
    public List<Link> Links { get; set; }
    public List<SubGraph>? SubGraphs { get; set; }
    public List<Node> NavigationNodes { get; set; }

    /// <summary>
    /// Initialize the flowchart
    /// </summary>
    /// <param name="direction">Currently accepts LR and TR options</param>
    /// <param name="nodes">A list of nodes</param>
    /// <param name="links">A list of links</param>
    public Flowchart(string direction, List<Node> nodes, List<Link> links, List<SubGraph>? subGraphs = null)
    {
        if (direction != "LR" && direction != "TD")
        {
            throw new NotSupportedException("Direction " + direction + " is currently unsupported");
        }
        else
        {
            Direction = direction;
        }
        Nodes = nodes;
        Links = links;
        SubGraphs = subGraphs;
        NavigationNodes = new();
        foreach (Node node in Nodes)
        {
            NavigationNodes.Add(node);
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
                NavigationNodes.AddRange(subGroup.Nodes);
                sb.Append("    ");
                sb.Append("subgraph ");
                sb.Append(subGroup.Name);
                sb.Append(Environment.NewLine);
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
        return sb.ToString();
    }

    private string AddLink(Link link)
    {
        StringBuilder sb = new();
        Node? sourceNode = NavigationNodes.Find(n => n.Name == link.SourceNode);
        Node? destinationNode = NavigationNodes.Find(n => n.Name == link.DestinationNode);
        if (sourceNode == null || destinationNode == null)
        {
            throw new ArgumentException("Nodes in link connection (" + link.SourceNode + "-->" + link.DestinationNode + ") not found");
        }
        sb.Append("    ");
        sb.Append(sourceNode.Name);
        sb.Append("--");
        if (!string.IsNullOrEmpty(link.Text))
        {
            sb.Append(link.Text);
            sb.Append("--");
        }
        sb.Append('>');
        sb.Append(destinationNode.Name);
        sb.Append(Environment.NewLine);
        return sb.ToString();
    }

}
