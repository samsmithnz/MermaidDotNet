namespace MermaidDotNet.Models
{
    public class Node
    {
        public Node(string name, string text, ShapeType shape = ShapeType.Rectangle)
        {
            Name = name.Replace(" ","");
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
                case ShapeType.Rectangle:
                    return "[";
                case ShapeType.Rounded:
                    return "(";
                case ShapeType.Stadium:
                    return "([";
                case ShapeType.Cylinder:
                    return "[(";
                case ShapeType.Circle:
                    return "((";
                case ShapeType.Rhombus:
                    return "{";
                case ShapeType.Hexagon:
                    return "{{";
                default: // Rectangle is default
                    return "[";
            }
        }

        public string CloseShape()
        {
            switch (Shape)
            {
                case ShapeType.Rectangle:
                    return "]";
                case ShapeType.Rounded:
                    return ")";
                case ShapeType.Stadium:
                    return "])";
                case ShapeType.Cylinder:
                    return ")]";
                case ShapeType.Circle:
                    return "))";
                case ShapeType.Rhombus:
                    return "}";
                case ShapeType.Hexagon:
                    return "}}";
                default: // Rectangle is default
                    return "]";
            }
        }
    }
}
