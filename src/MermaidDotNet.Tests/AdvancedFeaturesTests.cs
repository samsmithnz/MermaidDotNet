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
        List<Node> nodes = new()
        {
            new("node1", "Styled Node", Node.ShapeType.Rectangle, "myClass")
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart LR
    node1[Styled Node]
    class node1 myClass
";

        //Act
        string result = flowchart.CalculateFlowchart();

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
        List<Node> nodes = new()
        {
            new("node1", "Clickable Node", Node.ShapeType.Rectangle, null, "https://example.com")
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart LR
    node1[Clickable Node]
    click node1 ""https://example.com""
";

        //Act
        string result = flowchart.CalculateFlowchart();

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
        List<Node> nodes = new()
        {
            new("node1", "Styled Clickable Node", Node.ShapeType.Rectangle, "highlightClass", "alert('Hello!')")
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart LR
    node1[Styled Clickable Node]
    class node1 highlightClass
    click node1 ""alert('Hello!')""
";

        //Act
        string result = flowchart.CalculateFlowchart();

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
        List<Node> nodes = new()
        {
            new("start", "Start", Node.ShapeType.Circle, "startClass", "console.log('Start clicked')"),
            new("process", "Process Data", Node.ShapeType.Rectangle),
            new("decision", "Is Valid?", Node.ShapeType.Rhombus, "decisionClass"),
            new("end", "End", Node.ShapeType.Circle)
        };
        List<Link> links = new()
        {
            new Link("start", "process", "", null, false, Link.LinkType.Normal),
            new Link("process", "decision", "validate", null, false, Link.LinkType.Dotted),
            new Link("decision", "end", "yes", "stroke:green,stroke-width:3px", false, Link.LinkType.Thick),
            new Link("decision", "process", "", null, false, Link.LinkType.Normal, Link.ArrowType.Circle)
        };
        Flowchart flowchart = new(direction, nodes, links);
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
    click start ""console.log('Start clicked')""
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }
}