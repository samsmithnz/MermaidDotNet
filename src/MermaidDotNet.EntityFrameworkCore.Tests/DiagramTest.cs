using MermaidDotNet.EntityFrameworkCore.Tests.Mock;
using Microsoft.EntityFrameworkCore;

namespace MermaidDotNet.EntityFrameworkCore.Tests
{
    [TestClass]
    public class DiagramTest
    {
        [TestMethod]
        public void GenerateDiagram()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContextMock>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new DatabaseContextMock(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Act
            var diagram = context.ToMermaidEntityDiagram();
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(result);
            Assert.Contains("erDiagram", result);
        }
    }
}