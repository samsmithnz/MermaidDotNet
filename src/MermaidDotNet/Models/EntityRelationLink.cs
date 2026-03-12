using MermaidDotNet.Enums;
using System;

namespace MermaidDotNet.Models
{
    public class EntityRelationLink : Link
    {
        public RelationType SourceRelation { get; }
        public RelationType DestinationRelation { get; }

        /// <summary>
        /// Crée un lien de relation entre deux entités avec types de relation Mermaid ER.
        /// </summary>
        public EntityRelationLink(
            string sourceNode,
            string destinationNode,
            string text,
            RelationType sourceRelation,
            RelationType destinationRelation)
            : base(
                sourceNode,
                destinationNode,
                text ?? GetMermaidRelationSyntax(sourceRelation, destinationRelation),
                null,
                false,
                LinkType.Normal,
                ArrowType.Normal)
        {
            SourceRelation = sourceRelation;
            DestinationRelation = destinationRelation;
        }

        /// <summary>
        /// Crée un lien simple entre deux entités (sans syntaxe Mermaid ER personnalisée).
        /// </summary>
        public EntityRelationLink(
            string sourceNode,
            string destinationNode,
            RelationType sourceRelation,
            RelationType destinationRelation)
            : this(
                sourceNode,
                destinationNode,
                null,
                sourceRelation,
                destinationRelation)
        {
        }

        public override string GetLinkString()
        {
            // Utilise la syntaxe Mermaid ER pour les relations
            string relationSyntax = GetMermaidRelationSyntax(SourceRelation, DestinationRelation);
            string label = !string.IsNullOrEmpty(Text) ? $" : \"{Text}\"" : string.Empty;
            return $"{SourceNode} {relationSyntax} {DestinationNode}{label}";
        }

        /// <summary>
        /// Génère la syntaxe Mermaid ER pour le lien selon les types de relation.
        /// </summary>
        private static string GetMermaidRelationSyntax(RelationType source, RelationType destination)
        {
            // Symboles Mermaid ER
            string left = source switch
            {
                RelationType.ZeroOrOne => "|o",
                RelationType.ExactlyOne => "||",
                RelationType.ZeroOrMore => "}o",
                RelationType.OneOrMore => "}|",
                _ => "||"
            };
            string right = destination switch
            {
                RelationType.ZeroOrOne => "o|",
                RelationType.ExactlyOne => "||",
                RelationType.ZeroOrMore => "o{",
                RelationType.OneOrMore => "|{",
                _ => "||"
            };
            return $"{left}--{right}";
        }
    }
}