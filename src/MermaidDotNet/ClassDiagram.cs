using System.Text;
using MermaidDotNet.Models;

namespace MermaidDotNet;

public class ClassDiagram
{
    public List<Class> Classes { get; set; }
    public List<Relationship> Relationships { get; set; }

    public ClassDiagram(List<Class> classes, List<Relationship> relationships)
    {
        Classes = classes;
        Relationships = relationships;
    }

    public string CalculateClassDiagram()
    {
        StringBuilder sb = new();
        sb.Append("classDiagram" + Environment.NewLine);

        foreach (var cls in Classes)
        {
            sb.Append($"    class {cls.Name} {{\n");
            foreach (var attribute in cls.Attributes)
            {
                sb.Append($"        {attribute.Type} {attribute.Name}\n");
            }
            foreach (var method in cls.Methods)
            {
                sb.Append($"        {method.ReturnType} {method.Name}({string.Join(", ", method.Parameters)})\n");
            }
            sb.Append("    }\n");
        }

        foreach (var relationship in Relationships)
        {
            sb.Append($"    {relationship.Source} {relationship.Type} {relationship.Target}\n");
        }

        return sb.ToString();
    }
}
