using MermaidDotNet.Models;

namespace MermaidDotNet.Tests;

[TestClass]
public class NodeShapeTests
{
    [TestMethod]
    public void ParallelogramNodeShapeFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "This is a parallelogram", Node.ShapeType.Parallelogram)
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart LR
    node1[/This is a parallelogram/]
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TrapezoidNodeShapeFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "This is a trapezoid", Node.ShapeType.Trapezoid)
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart LR
    node1[\This is a trapezoid\]
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TrapezoidAltNodeShapeFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "This is a trapezoid alt", Node.ShapeType.TrapezoidAlt)
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart LR
    node1[/This is a trapezoid alt\]
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SubroutineNodeShapeFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "This is a subroutine", Node.ShapeType.Subroutine)
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart LR
    node1[[This is a subroutine]]
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }
}