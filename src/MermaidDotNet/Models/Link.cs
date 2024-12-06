namespace MermaidDotNet.Models
{
    public class Link
    {
        public Link(string sourceNode, string destinationNode, string? text = null, string? linkstyle = null, bool bidirectional = false)
        {
            SourceNode = sourceNode.Replace(" ", "");
            DestinationNode = destinationNode.Replace(" ", "");
            Text = text;
            IsBidirectional = bidirectional;
            LinkStyle = linkstyle;
        }

        public string SourceNode { get; set; }
        public string DestinationNode { get; set; }
        public string? Text { get; set; }
        public string? LinkStyle { get; set; }
        public bool IsBidirectional { get; }
    }
}
