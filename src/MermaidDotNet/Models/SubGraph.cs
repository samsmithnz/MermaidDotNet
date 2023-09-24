namespace MermaidDotNet.Models
{
    public class SubGraph
    {
        public string Name { get; set; }
        //Can be TB, BT, RL, LR (default: LR) (Top/Bottom/Left/Right, respectively)
        public string? Direction { get; set; } = null;
        public List<Node> Nodes { get; set; } = new();
        public List<Link> Links { get; set; } = new();

        public SubGraph(string name, string? direction = null)
        {
            Name = name.Replace(" ", "");
            Direction = direction;
        }
        public SubGraph(string name, List<Node> nodes, List<Link> links, string? direction = null)
        {
            Name = name.Replace(" ", "");
            Nodes = nodes;
            Links = links;
            Direction = direction;
        }   
    }
}
