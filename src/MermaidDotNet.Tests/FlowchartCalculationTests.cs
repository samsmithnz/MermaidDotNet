using MermaidDotNet.Models;
using System.Xml.Linq;

namespace MermaidDotNet.Tests;

[TestClass]
public class FlowchartCalculationTests
{
    [TestMethod]
    public void ValidTDFlowchart()
    {
        //Arrange
        string direction = "LR";
        Flowchart flowchart = new(direction, new(), new());
        string expected = @"flowchart LR
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SingleNodeFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "This is node 1")
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart LR
    node1[This is node 1]
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TwoNodesFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "This is node 1"),
            new("node2", "This is node 2")
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TwoNodesAndLinkFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "This is node 1"),
            new("node2", "This is node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", null)
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1-->node2
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TwoNodesAndLinkTextFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "This is node 1"),
            new("node2", "This is node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "link text!")
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1--link text!-->node2
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ThreeNodesAndTwoLinksFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "This is node 1"),
            new("node2", "This is node 2"),
            new("node3", "This is node 3")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "12s"),
            new Link("node1", "node3", "3mins")
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node3[This is node 3]
    node1--12s-->node2
    node1--3mins-->node3
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }


}