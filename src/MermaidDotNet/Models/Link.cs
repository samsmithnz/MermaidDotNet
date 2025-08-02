namespace MermaidDotNet.Models
{
    public class Link
    {
        public Link(string sourceNode, string destinationNode, string? text = null, string? linkstyle = null, bool isBidirectional = false, LinkType linkType = LinkType.Normal, ArrowType arrowType = ArrowType.Normal)
        {
            SourceNode = sourceNode.Replace(" ", "");
            DestinationNode = destinationNode.Replace(" ", "");
            Text = text;
            IsBidirectional = isBidirectional;
            LinkStyle = linkstyle;
            Type = linkType;
            Arrow = arrowType;
        }

        public string SourceNode { get; set; }
        public string DestinationNode { get; set; }
        public string? Text { get; set; }
        public string? LinkStyle { get; set; }
        public bool IsBidirectional { get; }
        public LinkType Type { get; set; }
        public ArrowType Arrow { get; set; }

        public enum LinkType
        {
            Normal,
            Dotted,
            Thick,
            Invisible
        }

        public enum ArrowType
        {
            Normal,
            Circle,
            Cross,
            Open
        }
    }
}
