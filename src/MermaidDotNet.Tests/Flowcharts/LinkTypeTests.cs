using MermaidDotNet.Diagrams;
using MermaidDotNet.Enums;
using MermaidDotNet.Models;

namespace MermaidDotNet.Tests.Flowcharts;

[TestClass]
public class LinkTypeTests
{
    [TestMethod]
    public void DottedLinkFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<FlowNode> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "dotted", null, false, LinkType.Dotted)
        };
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1-.dotted.->node2";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ThickLinkFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<FlowNode> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "thick", null, false, LinkType.Thick)
        };
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1==thick==>node2";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void InvisibleLinkFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<FlowNode> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "", null, false, LinkType.Invisible)
        };
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1~~~>node2";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void CircleArrowLinkFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<FlowNode> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "", null, false, LinkType.Normal, ArrowType.Circle)
        };
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1--onode2";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void CrossArrowLinkFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<FlowNode> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "", null, false, LinkType.Normal, ArrowType.Cross)
        };
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1--xnode2";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void BidirectionalCircleArrowLinkFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<FlowNode> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "", null, true, LinkType.Normal, ArrowType.Circle)
        };
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1o--onode2";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }
}