namespace MermaidDotNet.Models
{
    public class Node
    {
        public Node(string name, string text, ShapeType shape = Shape.Rectangle)
        {
            Name = name;
            Text = text;
            Dependencies = new();
            Shape = shape;
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public List<Node> Dependencies { get; set; }
        public ShapeType Shape { get; set; }

        public enum ShapeType
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
                case Shape.Rectangle:
                    return "[";
                case Shape.Rounded:
                    return "(";
                case Shape.Stadium:
                    return "([";
                case Shape.Cylinder:
                    return "[(";
                case Shape.Circle:
                    return "((";
                case Shape.Rhombus:
                    return "{";
                case Shape.Hexagon:
                    return "{{";
                default: // Rectangle is default
                    return "[";
            }
        }

        public string CloseShape()
        {
            switch (Shape)
            {
                case Shape.Rectangle:
                    return "]";
                case Shape.Rounded:
                    return ")";
                case Shape.Stadium:
                    return "])";
                case Shape.Cylinder:
                    return ")]";
                case Shape.Circle:
                    return "))";
                case Shape.Rhombus:
                    return "}";
                case Shape.Hexagon:
                    return "}}";
                default: // Rectangle is default
                    return "]";
            }
        }
    }
}
