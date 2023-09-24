namespace MermaidDotNet.Models
{
    public class SubGraph
    {
        public string Name { get; set; }
        public List<Node> Nodes { get; set; } = new();
        public List<Link> Links { get; set; } = new();

        public SubGraph(string name)
        {
            Name = name.Replace(" ", "");
        }
        public SubGraph(string name, List<Node> nodes, List<Link> links)
        {
            Name = name.Replace(" ", "");
            Nodes = nodes;
            Links = links;
        }   
    }
}
