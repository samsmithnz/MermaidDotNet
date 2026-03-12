using MermaidDotNet.Diagrams;
using MermaidDotNet.EntityFrameworkCore.Extensions;
using MermaidDotNet.EntityFrameworkCore.Models;
using MermaidDotNet.Enums;
using MermaidDotNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;

namespace MermaidDotNet.EntityFrameworkCore
{
    public static class EntityRelationshipDiagramExtension
    {
        public static EntityRelationshipDiagram ToMermaidEntityDiagram(this DbContext dbContext)
        {
            return dbContext.ToMermaidEntityDiagram(new EntityRelationshipDiagramOptions());
        }
        public static EntityRelationshipDiagram ToMermaidEntityDiagram(this DbContext dbContext, EntityRelationshipDiagramOptions options)
        {
            var entityTypes = dbContext.Model.GetEntityTypes()
                .Where(e => !e.IsOwned())
                .ToList();

            var schema = BuildSchema(entityTypes);
            return BuildEntityRelationshipDiagram(schema, options);
        }

        private static RelationType GetRelationType(IForeignKey fk)
        {
            if (fk.IsUnique)
            {
                // Relation 1:1 ou 0:1
                return fk.IsRequired ? RelationType.ExactlyOne : RelationType.ZeroOrOne;
            }
            else
            {
                // Relation 1:N ou 0:N
                return fk.IsRequired ? RelationType.OneOrMore : RelationType.ZeroOrMore;
            }
        }
        private static DiagramSchema BuildSchema(List<IEntityType> entityTypes)
        {
            var schema = new DiagramSchema();
            schema.Tables = entityTypes
                .Select(BuildTable)
                .ToList();

            var entityTables = schema.Tables
                .ToDictionary(t => t.EntityType);

            foreach (var tableKey in entityTables)
            {
                var table = tableKey.Value;
                var foreignKeys = table.Columns
                    .Where(c => c.ColumnKeyType.HasFlag(ColumnKeyType.ForeignKey))
                    .ToList();

                foreach (var fk in foreignKeys)
                {
                    var foreignKey = fk.Property.GetContainingForeignKeys().FirstOrDefault();
                    if (foreignKey == null)
                        continue;

                    var pk = foreignKey.PrincipalKey.Properties.FirstOrDefault();
                    if (!(pk.DeclaringType is IEntityType pkDeclaringType))
                    {
                        continue;
                    }

                    schema.Links.Add(new DiagramLink
                    {
                        Source = table,
                        Target = entityTables[pkDeclaringType],
                        Label = fk.Property.Name,
                        SourceType = GetRelationType(foreignKey),
                        TargetType = RelationType.ExactlyOne,
                        DeleteBehavior = foreignKey.DeleteBehavior
                    });
                }
            }

            return schema;
        }
        private static DiagramTable BuildTable(IEntityType entityType)
        {
            var pkPropertyNames = entityType.FindPrimaryKey()?.Properties
                .Select(p => p.Name)
                .ToHashSet() ?? [];

            var fkPropertyNames = entityType.GetForeignKeys()
                .SelectMany(fk => fk.Properties)
                .Select(p => p.Name)
                .ToHashSet();

            var table = new DiagramTable
            {
                Name = entityType.ClrType.Name,
                EntityType = entityType
            };

            foreach (var property in entityType.GetProperties())
            {
                var referenceType = pkPropertyNames.Contains(property.Name) ? ColumnKeyType.PrimaryKey : ColumnKeyType.None;
                referenceType |= fkPropertyNames.Contains(property.Name) ? ColumnKeyType.ForeignKey : ColumnKeyType.None;
                referenceType |= property.IsUniqueIndex() ? ColumnKeyType.UniqueKey : ColumnKeyType.None;

                table.Columns.Add(new DiagramColumn
                {
                    Property = property,
                    Name = property.Name,
                    Type = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType,
                    IsNullable = property.IsNullable,
                    ColumnKeyType = referenceType
                });
            }

            // Include owned entity properties inline
            foreach (var navigation in entityType.GetNavigations()
                .Where(n => n.TargetEntityType.IsOwned()))
            {
                var owned = navigation.TargetEntityType;
                foreach (var property in owned.GetProperties()
                    .Where(p => !p.IsForeignKey() && !p.IsPrimaryKey()))
                {
                    var referenceType = property.IsUniqueIndex() ? ColumnKeyType.UniqueKey : ColumnKeyType.None;
                    table.Columns.Add(new DiagramColumn
                    {
                        Property = property,
                        Name = $"{navigation.Name}_{property.Name}",
                        Type = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType,
                        IsNullable = property.IsNullable,
                        ColumnKeyType = referenceType
                    });
                }
            }

            return table;
        }

        private static EntityRelationshipDiagram BuildEntityRelationshipDiagram(DiagramSchema schema, EntityRelationshipDiagramOptions options)
        {
            // Création des nœuds à partir des tables
            var nodes = BuildEntityRelationNodes(schema, options);

            // Création des liens à partir des DiagramLinks
            var links = BuildEntityRelationLinks(schema, options);

            // Construction du diagramme
            return new EntityRelationshipDiagram(nodes, links);
        }

        private static List<EntityRelationNode> BuildEntityRelationNodes(DiagramSchema schema, EntityRelationshipDiagramOptions options)
        {
            var nodes = new List<EntityRelationNode>();
            foreach (var table in schema.Tables)
            {
                nodes.Add(new EntityRelationNode(
                    table.Name,
                    BuildEntityRelationColumns(table, options)
                ));
            }
            return nodes;
        }

        private static List<EntityRelationColumn> BuildEntityRelationColumns(DiagramTable table, EntityRelationshipDiagramOptions options)
        {
            var columns = new List<EntityRelationColumn>();

            if (!options.IncludeColumns)
            {
                return columns;
            }

            var filteredColumns = options.FilterColumnByKeyTypes != ColumnKeyType.None
                ? table.Columns.Where(c => c.ColumnKeyType != ColumnKeyType.None && options.FilterColumnByKeyTypes.HasFlag(c.ColumnKeyType))
                : table.Columns;
            foreach (var column in filteredColumns)
            {
                var erColumn = new EntityRelationColumn(
                    column.Name,
                    column.Type.Name,
                    options.IncludeColumnKeyTypes ? column.ColumnKeyType : ColumnKeyType.None,
                    options.IncludeColumnComments ? column.Property.GetDescription() : string.Empty
                );
                columns.Add(erColumn);
            }

            return columns;
        }

        private static List<EntityRelationLink> BuildEntityRelationLinks(DiagramSchema schema, EntityRelationshipDiagramOptions options)
        {
            var links = new List<EntityRelationLink>();

            if (!options.IncludeLinks)
            {
                return links;
            }

            foreach (var link in schema.Links)
            {
                var relationLabel = string.Empty;

                if (options.IncludeLinkLabels)
                {
                    relationLabel = link.Label;
                }

                if (options.IncludeLinkDeleteBehaviors)
                {
                    relationLabel = string.Join(" ", relationLabel, $"({link.DeleteBehavior.ToString()})");
                }

                links.Add(new EntityRelationLink(
                    link.Source.Name,
                    link.Target.Name,
                    relationLabel,
                    link.SourceType,
                    link.TargetType
                ));
            }
            return links;
        }
    }
}