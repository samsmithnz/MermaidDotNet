using MermaidDotNet.Enums;
using MermaidDotNet.Models;

namespace MermaidDotNet.Tests;

[TestClass]
public class AdvancedFeaturesTests
{
    [TestMethod]
    public void NodeWithCssClassFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<FlowNode> nodes = new()
        {
            new("node1", "Styled Node", ShapeType.Rectangle, "myClass")
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart LR
    node1[Styled Node]
    class node1 myClass";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void NodeWithClickActionFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<FlowNode> nodes = new()
        {
            new("node1", "Clickable Node", ShapeType.Rectangle, null, "https://example.com")
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart LR
    node1[Clickable Node]
    click node1 ""https://example.com""";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void NodeWithBothClassAndClickFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<FlowNode> nodes = new()
        {
            new("node1", "Styled Clickable Node", ShapeType.Rectangle, "highlightClass", "alert('Hello!')")
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart LR
    node1[Styled Clickable Node]
    class node1 highlightClass
    click node1 ""alert('Hello!')""";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ComplexFlowchartWithAllFeatures()
    {
        //Arrange
        string direction = "TD";
        List<FlowNode> nodes = new()
        {
            new("start", "Start", ShapeType.Circle, "startClass", "console.log('Start clicked')"),
            new("process", "Process Data", ShapeType.Rectangle),
            new("decision", "Is Valid?", ShapeType.Rhombus, "decisionClass"),
            new("end", "End", ShapeType.Circle)
        };
        List<Link> links = new()
        {
            new Link("start", "process", "", null, false, LinkType.Normal),
            new Link("process", "decision", "validate", null, false, LinkType.Dotted),
            new Link("decision", "end", "yes", "stroke:green,stroke-width:3px", false, LinkType.Thick),
            new Link("decision", "process", "", null, false, LinkType.Normal, ArrowType.Circle)
        };
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart TD
    start((Start))
    process[Process Data]
    decision{Is Valid?}
    end((End))
    start-->process
    process-.validate.->decision
    decision==yes==>end
    decision--oprocess
    linkStyle 0 stroke:green,stroke-width:3px
    class start startClass
    class decision decisionClass
    click start ""console.log('Start clicked')""";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }
}