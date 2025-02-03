using MermaidDotNet.Models;

namespace MermaidDotNet.Tests;

[TestClass]
public class ClassDiagramTests
{
    [TestMethod]
    public void ValidClassDiagram()
    {
        // Arrange
        List<Class> classes = new()
        {
            new Class("Class1", new List<Attribute>(), new List<Method>())
        };
        List<Relationship> relationships = new();
        ClassDiagram classDiagram = new(classes, relationships);
        string expected = @"classDiagram
    class Class1 {
    }
";

        // Act
        string result = classDiagram.CalculateClassDiagram();

        // Assert
        Assert.IsNotNull(classDiagram);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ClassWithAttributesAndMethods()
    {
        // Arrange
        List<Attribute> attributes = new()
        {
            new Attribute("Attribute1", "string"),
            new Attribute("Attribute2", "int")
        };
        List<Method> methods = new()
        {
            new Method("Method1", "void", new List<string> { "param1", "param2" }),
            new Method("Method2", "int", new List<string> { "param1" })
        };
        List<Class> classes = new()
        {
            new Class("Class1", attributes, methods)
        };
        List<Relationship> relationships = new();
        ClassDiagram classDiagram = new(classes, relationships);
        string expected = @"classDiagram
    class Class1 {
        string Attribute1
        int Attribute2
        void Method1(param1, param2)
        int Method2(param1)
    }
";

        // Act
        string result = classDiagram.CalculateClassDiagram();

        // Assert
        Assert.IsNotNull(classDiagram);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }
}
