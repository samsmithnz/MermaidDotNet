using MermaidDotNet.Diagrams;
using MermaidDotNet.Enums;
using MermaidDotNet.Models;

namespace MermaidDotNet.Tests.Flowcharts;

[TestClass]
public class NodeShapeTests
{
    [TestMethod]
    public void ParallelogramNodeShapeFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<FlowNode> nodes = new()
        {
            new("node1", "This is a parallelogram", ShapeType.Parallelogram)
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart LR
    node1[/This is a parallelogram/]";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is a trapezoid", ShapeType.Trapezoid)
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart LR
    node1[\This is a trapezoid\]";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is a trapezoid alt", ShapeType.TrapezoidAlt)
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart LR
    node1[/This is a trapezoid alt\]";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is a subroutine", ShapeType.Subroutine)
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart LR
    node1[[This is a subroutine]]";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }
}