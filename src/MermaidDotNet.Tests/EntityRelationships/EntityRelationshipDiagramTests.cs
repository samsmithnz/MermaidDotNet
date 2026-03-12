using MermaidDotNet.Diagrams;
using MermaidDotNet.Enums;
using MermaidDotNet.Models;

namespace MermaidDotNet.Tests.EntityRelationships
{
    [TestClass]
    public class EntityRelationshipDiagramTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeNodesAndLinks_WithComplexDatabase()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("User", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("Id", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("Name", "string"),
                    new EntityRelationColumn("Email", "string")
                }),
                new EntityRelationNode("Order", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("OrderId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("UserId", "int", ColumnKeyType.ForeignKey),
                    new EntityRelationColumn("OrderDate", "datetime")
                }),
                new EntityRelationNode("Product", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("ProductId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("Title", "string"),
                    new EntityRelationColumn("Price", "decimal")
                }),
                new EntityRelationNode("OrderItem", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("OrderItemId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("OrderId", "int", ColumnKeyType.ForeignKey),
                    new EntityRelationColumn("ProductId", "int", ColumnKeyType.ForeignKey),
                    new EntityRelationColumn("Quantity", "int")
                }),
                new EntityRelationNode("Category", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("CategoryId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("Name", "string")
                }),
                new EntityRelationNode("ProductCategory", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("ProductId", "int", ColumnKeyType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("CategoryId", "int", ColumnKeyType.PrimaryKeyForeignKey)
                })
            };

            var links = new List<EntityRelationLink>
            {
                new EntityRelationLink("User", "Order", "UserId (Cascade)", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Order", "OrderItem", "OrderId", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Product", "OrderItem", "ProductId", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Product", "ProductCategory", "ProductId", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Category", "ProductCategory", "CategoryId", RelationType.OneOrMore, RelationType.ZeroOrMore)
            };

            var expected = @"erDiagram
    User {
        int Id PK
        string Name
        string Email
    }
    Order {
        int OrderId PK
        int UserId FK
        datetime OrderDate
    }
    Product {
        int ProductId PK
        string Title
        decimal Price
    }
    OrderItem {
        int OrderItemId PK
        int OrderId FK
        int ProductId FK
        int Quantity
    }
    Category {
        int CategoryId PK
        string Name
    }
    ProductCategory {
        int ProductId PK, FK
        int CategoryId PK, FK
    }
    User }|--o{ Order : ""UserId (Cascade)""
    Order }|--o{ OrderItem : ""OrderId""
    Product }|--o{ OrderItem : ""ProductId""
    Product }|--o{ ProductCategory : ""ProductId""
    Category }|--o{ ProductCategory : ""CategoryId""";

            // Act
            var diagram = new EntityRelationshipDiagram(nodes, links);
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldReturnEmptyDiagram_WhenNoNodesOrLinks()
        {
            // Arrange
            var diagram = new EntityRelationshipDiagram(new List<EntityRelationNode>(), new List<EntityRelationLink>());

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual("erDiagram", diagram.Name);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldHandleSingleNodeWithoutLinks()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("Customer", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("CustomerId", "int"),
                    new EntityRelationColumn("FullName", "string")
                })
            };
            var expected = @"erDiagram
    Customer {
        int CustomerId
        string FullName
    }";

            // Act
            var diagram = new EntityRelationshipDiagram(nodes, new List<EntityRelationLink>());
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldHandleCircularRelations()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("Person", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("PersonId", "int", ColumnKeyType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("ManagerId", "int", ColumnKeyType.PrimaryKeyForeignKey)
                })
            };
            var links = new List<EntityRelationLink>
            {
                new EntityRelationLink("Person", "Person", "ManagerId", RelationType.ZeroOrOne, RelationType.ZeroOrMore)
            };
            var expected = @"erDiagram
    Person {
        int PersonId PK, FK
        int ManagerId PK, FK
    }
    Person |o--o{ Person : ""ManagerId""";

            // Act
            var diagram = new EntityRelationshipDiagram(nodes, links);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldHandleMultipleLinksBetweenSameEntities()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("Author", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("AuthorId", "int"),
                    new EntityRelationColumn("Name", "string") 
                }),
                new EntityRelationNode("Book", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("BookId", "int"),
                    new EntityRelationColumn("Title", "string"),
                    new EntityRelationColumn("AuthorId", "int"),
                    new EntityRelationColumn("EditorId", "int")
                })
            };
            var links = new List<EntityRelationLink>
            {
                new EntityRelationLink("Author", "Book", "AuthorId", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Author", "Book", "EditorId", RelationType.OneOrMore, RelationType.ZeroOrMore)
            };
            var expected = @"erDiagram
    Author {
        int AuthorId
        string Name
    }
    Book {
        int BookId
        string Title
        int AuthorId
        int EditorId
    }
    Author }|--o{ Book : ""AuthorId""
    Author }|--o{ Book : ""EditorId""";

            // Act
            var diagram = new EntityRelationshipDiagram(nodes, links);
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}
