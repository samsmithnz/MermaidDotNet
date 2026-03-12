namespace MermaidDotNet.Enums
{
    /// <summary>
    /// Types de relations pour les diagrammes ER Mermaid.
    /// </summary>
    public enum RelationType
    {
        /// <summary>
        /// Zero ou un (|o o|).
        /// </summary>
        ZeroOrOne,

        /// <summary>
        /// Exactement un (|| ||).
        /// </summary>
        ExactlyOne,

        /// <summary>
        /// Zero ou plusieurs (}o o{).
        /// </summary>
        ZeroOrMore,

        /// <summary>
        /// Un ou plusieurs (}| |{).
        /// </summary>
        OneOrMore
    }
}