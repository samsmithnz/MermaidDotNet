namespace MermaidDotNet.Models
{
    public class Link
    {
        public Link(string sourceNode, string destinationNode, string text)
        {
            SourceNode = sourceNode;
            DestinationNode = destinationNode;
            Text = text;
        }

        public string SourceNode { get; set; }
        public string DestinationNode { get; set; }
        public string Text { get; set; }
    }
}
