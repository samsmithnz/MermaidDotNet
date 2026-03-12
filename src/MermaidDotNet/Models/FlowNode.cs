using MermaidDotNet.Enums;

namespace MermaidDotNet.Models
{
    public class FlowNode : Node
    {
        public FlowNode(string name, string text, ShapeType shape = ShapeType.Rectangle, string? cssClass = null, string? clickAction = null)
            : base(name, text, cssClass)
        {
            Shape = shape;
            ClickAction = clickAction;
        }

        public ShapeType Shape { get; set; }
        public string? ClickAction { get; set; }

        public string GetClickActionString()
        {
            if (string.IsNullOrEmpty(ClickAction))
            {
                return string.Empty;
            }
            return $"click {Name} \"{ClickAction}\"";
        }
        protected override string GetSurroundedText()
        {
            return $"{OpenShape()}{Text}{CloseShape()}";
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
                case ShapeType.Parallelogram:
                    return "[/";
                case ShapeType.Trapezoid:
                    return "[\\";
                case ShapeType.TrapezoidAlt:
                    return "[/";
                case ShapeType.Subroutine:
                    return "[[";
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
                case ShapeType.Parallelogram:
                    return "/]";
                case ShapeType.Trapezoid:
                    return "\\]";
                case ShapeType.TrapezoidAlt:
                    return "\\]";
                case ShapeType.Subroutine:
                    return "]]";
                default: // Rectangle is default
                    return "]";
            }
        }
    }
}
