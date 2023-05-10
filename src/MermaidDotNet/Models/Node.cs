namespace MermaidDotNet.Models
{
    public class Node
    {
        public Node(string name, string text, eShape shape = eShape.Rectangle)
        {
            Name = name;
            Text = text;
            Dependencies = new();
            Shape = shape;
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public List<Node> Dependencies { get; set; }
        public eShape Shape { get; set; }

        public enum eShape
        {
            Rectangle,
            Rounded,
            Stadium,
            Cylinder,
            Circle,
            Rhombus,
            Hexagon
        }
    }
}
