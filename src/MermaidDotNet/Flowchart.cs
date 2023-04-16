using MermaidDotNet.Models;
using System.Text;
using System.Xml.Linq;

namespace MermaidDotNet;
public class Flowchart
{
    public string Direction { get; set; }
    public List<Node> Nodes { get; set; }
    public List<Link> Links { get; set; }

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

    public string CalculateFlowchart()
    {
        StringBuilder sb = new();
        sb.Append("flowchart " + Direction + Environment.NewLine);
        //foreach (Node node in nodes)
        //{
        //    sb.Append(node.Name);
        //    sb.Append("[");
        //    sb.Append(node.Text);
        //    sb.Append("]");
        //    if (links.Where(p => p.SourceNode == node.Name).FirstOrDefault() != null)
        //    {

        //    }

        //    sb.Append(Environment.NewLine);
        //    //miner1["Miner Mk1<br>(Iron Ore)"]--"Iron Ore<br>(60 units/min)"--> smeltor1
        //}
        foreach (Node node in Nodes)
        {
            sb.Append("    ");
            sb.Append(node.Name);
            sb.Append("[");
            sb.Append(node.Text);
            sb.Append("]");
            sb.Append(Environment.NewLine);
        }
        foreach (Link link in Links)
        {
            Node? sourceNode = Nodes.Where(n => n.Name == link.SourceNode).FirstOrDefault();
            Node? destinationNode = Nodes.Where(n => n.Name == link.DestinationNode).FirstOrDefault();
            if (sourceNode == null || destinationNode == null)
            {
                throw new Exception("Nodes in link connection (" + link.SourceNode + "-->" + link.DestinationNode + ") not found");
            }

            sb.Append("    ");
            //if (sourceNode != null)
            //{
            //    sb.Append(sourceNode.Name);
            //    sb.Append("[");
            //    sb.Append(sourceNode.Text);
            //    sb.Append("]");
            //}
            sb.Append(sourceNode.Name);
            sb.Append("--");
            if (string.IsNullOrEmpty(link.Text) == false)
            {
                sb.Append(link.Text);
                sb.Append("--");
            }
            sb.Append(">");
            sb.Append(destinationNode.Name);

            //if (destinationNode != null)
            //{
            //    sb.Append(destinationNode.Name);
            //}
            sb.Append(Environment.NewLine);
        }
        return sb.ToString();
    }

}
