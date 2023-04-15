using System.Text;

namespace MermaidDotNet;
public class Flowchart
{
    public string Direction { get; set; }

    public Flowchart(string direction)
    {
        if (direction != "LR" && direction != "TD")
        {
            throw new Exception("Direction " + direction + " is currently unsupported");
        }
        else
        {
            Direction = direction;
        }
    }

    public string CalculateFlowchart()
    {
        StringBuilder sb = new();
        sb.Append("flowchart " + Direction);

        return sb.ToString();
    }

}
