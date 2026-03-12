using MermaidDotNet.Diagrams;
using MermaidDotNet.EntityFrameworkCore.Models;
using MermaidDotNet.Enums;
using MermaidDotNet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MermaidDotNet.EntityFrameworkCore
{
    public static class EntityRelationshipDiagramExtension
    {
        public static EntityRelationshipDiagram ToMermaidEntityDiagram(this DbContext dbContext)
        {
            var entityTypes = dbContext.Model.GetEntityTypes()
                .Where(e => !e.IsOwned())
                .ToList();

            var schema = BuildSchema(entityTypes);
            return BuildEntityRelationshipDiagram(schema);
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

        private static EntityRelationshipDiagram BuildEntityRelationshipDiagram(DiagramSchema schema)
        {
            // Création des nœuds à partir des tables
            var nodes = schema.Tables.Select(BuildTableNode).ToList();

            // Création des liens à partir des DiagramLink
            var links = schema.Links.Select(link =>
                new EntityRelationLink(
                    link.Source.Name,
                    link.Target.Name,
                    string.Join(" ", link.Label, $"({link.DeleteBehavior.ToString()})"),
                    link.SourceType,
                    link.TargetType
                )
            ).ToList();

            // Construction du diagramme
            return new EntityRelationshipDiagram(nodes, links);
        }
        private static EntityRelationNode BuildTableNode(DiagramTable table)
        {
            var columnsEntities = table.Columns
                .Select(c => new EntityRelationColumn(c.Name, c.Type.Name, c.ColumnKeyType))
                .ToList();

            return new EntityRelationNode(table.Name, columnsEntities);
        }

    }
}