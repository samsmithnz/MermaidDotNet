namespace MermaidDotNet.Models
{
    public class Node
    {
        public Node(string name, string text, ShapeEnum shape = ShapeEnum.Rectangle)
        {
            Name = name;
            Text = text;
            Dependencies = new();
            Shape = shape;
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public List<Node> Dependencies { get; set; }
        public ShapeEnum Shape { get; set; }

        public enum ShapeEnum
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
                case ShapeEnum.Rectangle:
                    return "[";
                case ShapeEnum.Rounded:
                    return "(";
                case ShapeEnum.Stadium:
                    return "([";
                case ShapeEnum.Cylinder:
                    return "[(";
                case ShapeEnum.Circle:
                    return "((";
                case ShapeEnum.Rhombus:
                    return "{";
                case ShapeEnum.Hexagon:
                    return "{{";
                default: // Rectangle is default
                    return "[";
            }
        }

        public string CloseShape()
        {
            switch (Shape)
            {
                case ShapeEnum.Rectangle:
                    return "]";
                case ShapeEnum.Rounded:
                    return ")";
                case ShapeEnum.Stadium:
                    return "])";
                case ShapeEnum.Cylinder:
                    return ")]";
                case ShapeEnum.Circle:
                    return "))";
                case ShapeEnum.Rhombus:
                    return "}";
                case ShapeEnum.Hexagon:
                    return "}}";
                default: // Rectangle is default
                    return "]";
            }
        }
    }
}
