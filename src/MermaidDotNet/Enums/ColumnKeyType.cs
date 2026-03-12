using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidDotNet.Enums
{
    [Flags]
    public enum ColumnKeyType
    {
        None = 0,
        PrimaryKey = 1,
        ForeignKey = 2,

        PrimaryKeyForeignKey = PrimaryKey | ForeignKey,

        UniqueKey = 4,
        Indexed = 8
    }
}
