using MermaidDotNet.Enums;
using System.Text;

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

        public virtual string GetLinkString()
        {
            StringBuilder sb = new();
            sb.Append(SourceNode);
            if (IsBidirectional)
            {
                sb.Append(GetStartArrowSymbol(Arrow));
            }

            sb.Append(GetLinkSymbol(Type));

            if (!string.IsNullOrEmpty(Text))
            {
                sb.Append(Text);
                var linkSymbol = GetLinkSymbol(Type);
                if (Type == LinkType.Dotted)
                {
                    linkSymbol = new string(linkSymbol.Reverse().ToArray());
                }
                sb.Append(linkSymbol);
            }

            sb.Append(GetEndArrowSymbol(Arrow));
            sb.Append(DestinationNode);
            return sb.ToString();
        }
        public string GetStyleString(int index)
        {
            if (string.IsNullOrEmpty(LinkStyle))
            {
                return string.Empty;
            }
            return $"linkStyle {index} {LinkStyle}";
        }

        private string GetLinkSymbol(LinkType linkType)
        {
            return linkType switch
            {
                LinkType.Normal => "--",
                LinkType.Dotted => "-.",
                LinkType.Thick => "==",
                LinkType.Invisible => "~~~",
                _ => "--"
            };
        }

        private string GetStartArrowSymbol(ArrowType arrowType)
        {
            return arrowType switch
            {
                ArrowType.Normal => "<",
                ArrowType.Circle => "o",
                ArrowType.Cross => "x",
                ArrowType.Open => "<",
                _ => "<"
            };
        }

        private string GetEndArrowSymbol(ArrowType arrowType)
        {
            return arrowType switch
            {
                ArrowType.Normal => ">",
                ArrowType.Circle => "o",
                ArrowType.Cross => "x",
                ArrowType.Open => ">",
                _ => ">"
            };
        }
    }
}
