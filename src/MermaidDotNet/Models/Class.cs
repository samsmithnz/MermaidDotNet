namespace MermaidDotNet.Models;

public class Class
{
    public string Name { get; set; }
    public List<Attribute> Attributes { get; set; }
    public List<Method> Methods { get; set; }

    public Class(string name, List<Attribute> attributes, List<Method> methods)
    {
        Name = name;
        Attributes = attributes;
        Methods = methods;
    }
}
