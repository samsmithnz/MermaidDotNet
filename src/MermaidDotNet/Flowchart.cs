using MermaidDotNet.Models;
using System.Text;

namespace MermaidDotNet;

public class Flowchart
{
    public string Direction { get; set; }
    public List<Node> Nodes { get; set; }
    public List<Link> Links { get; set; }

    /// <summary>
    /// Initialize the flowchart
    /// </summary>
    /// <param name="direction">Currently accepts LR and TR options</param>
    /// <param name="nodes">A list of nodes</param>
    /// <param name="links">A list of links</param>
    public Flowchart(string direction, List<Node> nodes, List<Link> links)
    {
        if (direction != "LR" && direction != "TD")
        {
            throw new Exception("Direction " + direction + " is currently unsupported");
        }
        else
        {
            Direction = direction;
        }
        Nodes = nodes;
        Links = links;
    }

    /// <summary>
    /// Given a list of nodes and links, calculate the mermaid flowchart
    /// </summary>
    /// <returns>a mermaid graph as a string</returns>
    public string CalculateFlowchart()
    {
        StringBuilder sb = new();
        sb.Append("flowchart " + Direction + Environment.NewLine);

        //Add nodes
        foreach (Node node in Nodes)
        {
            sb.Append("    ");
            sb.Append(node.Name);
            sb.Append(node.OpenShape());
            sb.Append(node.Text);
            sb.Append(node.CloseShape());
            sb.Append(Environment.NewLine);
        }

        //Add links
        foreach (Link link in Links)
        {
            Node? sourceNode = Nodes.Where(n => n.Name == link.SourceNode).FirstOrDefault();
            Node? destinationNode = Nodes.Where(n => n.Name == link.DestinationNode).FirstOrDefault();
            if (sourceNode == null || destinationNode == null)
            {
                throw new Exception("Nodes in link connection (" + link.SourceNode + "-->" + link.DestinationNode + ") not found");
            }
            sb.Append("    ");
            sb.Append(sourceNode.Name);
            sb.Append("--");
            if (string.IsNullOrEmpty(link.Text) == false)
            {
                sb.Append(link.Text);
                sb.Append("--");
            }
            sb.Append(">");
            sb.Append(destinationNode.Name);
            sb.Append(Environment.NewLine);
        }
        return sb.ToString();
    }

}
