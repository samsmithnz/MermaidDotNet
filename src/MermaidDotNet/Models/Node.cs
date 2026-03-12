using System.Text;

namespace MermaidDotNet.Models
{
    public class Node
    {
        public Node(string name, string text, string? cssClass = null)
        {
            Name = name.Replace(" ", "");
            Text = text;
            CssClass = cssClass;
        }

        public string Name { get; set; }
        public string Text { get; set; }
        public string? CssClass { get; set; }

        public virtual string GetNodeString()
        {
            var sb = new StringBuilder();
            sb.Append(Name);

            if (!string.IsNullOrEmpty(Text))
            {
                sb.Append(GetSurroundedText());
            }

            return sb.ToString();
        }
        public virtual string GetClassString()
        {
            if (string.IsNullOrEmpty(CssClass))
            {
                return string.Empty;
            }

            return $"class {Name} {CssClass}";
        }
        protected virtual string GetSurroundedText()
        {
            return $"[{Text}]";
        }
    }
}
