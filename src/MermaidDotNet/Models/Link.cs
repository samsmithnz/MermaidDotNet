namespace MermaidDotNet.Models
{
    public class Link
    {
        public Link(string sourceNode, string destinationNode, string? text = null)
        {
            SourceNode = sourceNode.Replace(" ", "");
            DestinationNode = destinationNode.Replace(" ", "");
            Text = text;
        }

        public string SourceNode { get; set; }
        public string DestinationNode { get; set; }
        public string? Text { get; set; }
    }
}
