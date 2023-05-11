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

        public string OpenShape()
        {
            switch (Shape)
            {
                case eShape.Rectangle:
                    return "[";
                case eShape.Rounded:
                    return "(";
                case eShape.Stadium:
                    return "([";
                case eShape.Cylinder:
                    return "[(";
                case eShape.Circle:
                    return "((";
                case eShape.Rhombus:
                    return "{";
                case eShape.Hexagon:
                    return "{{";
                default: // Rectangle is default
                    return "[";
            }
        }

        public string CloseShape()
        {
            switch (Shape)
            {
                case eShape.Rectangle:
                    return "]";
                case eShape.Rounded:
                    return ")";
                case eShape.Stadium:
                    return "])";
                case eShape.Cylinder:
                    return ")]";
                case eShape.Circle:
                    return "))";
                case eShape.Rhombus:
                    return "}";
                case eShape.Hexagon:
                    return "}}";
                default: // Rectangle is default
                    return "]";
            }
        }
    }
}
