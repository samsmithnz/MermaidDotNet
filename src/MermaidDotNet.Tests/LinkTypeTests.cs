using MermaidDotNet.Models;

namespace MermaidDotNet.Tests;

[TestClass]
public class LinkTypeTests
{
    [TestMethod]
    public void DottedLinkFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "dotted", null, false, Link.LinkType.Dotted)
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1-.dotted.->node2
";

        //Act
        string result = flowchart.CalculateFlowchart();

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
        List<Node> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "thick", null, false, Link.LinkType.Thick)
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1==thick==>node2
";

        //Act
        string result = flowchart.CalculateFlowchart();

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
        List<Node> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "", null, false, Link.LinkType.Invisible)
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1~~~>node2
";

        //Act
        string result = flowchart.CalculateFlowchart();

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
        List<Node> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "", null, false, Link.LinkType.Normal, Link.ArrowType.Circle)
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1--onode2
";

        //Act
        string result = flowchart.CalculateFlowchart();

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
        List<Node> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "", null, false, Link.LinkType.Normal, Link.ArrowType.Cross)
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1--xnode2
";

        //Act
        string result = flowchart.CalculateFlowchart();

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
        List<Node> nodes = new()
        {
            new("node1", "Node 1"),
            new("node2", "Node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "", null, true, Link.LinkType.Normal, Link.ArrowType.Circle)
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[Node 1]
    node2[Node 2]
    node1o--onode2
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }
}