namespace MermaidDotNet.Models
{
    public class Node
    {
        public Node(string name, string text, string shape = "")
        {
            Name = name;
            Text = text;
            Dependencies = new();
            Shape = shape;
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public List<Node> Dependencies { get; set; }
        public string Shape { get; set; }
    }
}
