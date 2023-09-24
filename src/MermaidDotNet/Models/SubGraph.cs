namespace MermaidDotNet.Models
{
    public class SubGraph
    {
        public string Name { get; set; }
        //Can be TB, BT, RL, LR (default: LR) (Top/Bottom/Left/Right, respectively)
        public string Direction { get; set; } = "LR";
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
