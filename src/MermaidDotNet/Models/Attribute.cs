namespace MermaidDotNet.Models;

public class Attribute
{
    public string Name { get; set; }
    public string Type { get; set; }

    public Attribute(string name, string type)
    {
        Name = name;
        Type = type;
    }
}
