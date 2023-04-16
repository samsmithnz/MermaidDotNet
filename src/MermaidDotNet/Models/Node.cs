namespace MermaidDotNet.Models
{
    public class Node
    {
        public Node(string name, string text)
        {
            Name = name;
            Text = text;
            Dependencies = new();
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public List<Node> Dependencies { get; set; }
    }
}
