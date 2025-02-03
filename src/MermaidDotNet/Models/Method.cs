namespace MermaidDotNet.Models;

public class Method
{
    public string Name { get; set; }
    public string ReturnType { get; set; }
    public List<string> Parameters { get; set; }

    public Method(string name, string returnType, List<string> parameters)
    {
        Name = name;
        ReturnType = returnType;
        Parameters = parameters;
    }
}
