using MermaidDotNet.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidDotNet.Extensions
{
    internal static class StringExtension
    {
        public static List<string> Indent(this IEnumerable<string> lst, int indent = 1)
        {
            string indentString = string.Concat(Enumerable.Repeat(FormattingConstants.Indentation, indent));
            return lst.Select(i => indentString + i.ToString()).ToList();
        }
        public static List<string> ClearNewLines(this IEnumerable<string> lst)
        {
            return lst.Where(i => !string.IsNullOrEmpty(i)).ToList();
        }
    }
}
