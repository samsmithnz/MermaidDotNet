namespace MermaidDotNet.Enums
{
    [Flags]
    public enum ColumnKeyType
    {
        None = 0,
        PrimaryKey = 1,
        ForeignKey = 2,
        UniqueKey = 4,

        PrimaryKeyForeignKey = PrimaryKey | ForeignKey,
        PrimaryKeyUniqueKey = PrimaryKey | UniqueKey,
        ForeignKeyUniqueKey = ForeignKey | UniqueKey,
        PrimaryKeyForeignKeyUniqueKey = PrimaryKey | ForeignKey | UniqueKey,
    }
}
