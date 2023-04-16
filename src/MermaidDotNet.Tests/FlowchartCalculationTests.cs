using MermaidDotNet.Models;

namespace MermaidDotNet.Tests;

[TestClass]
public class FlowchartCalculationTests
{
    [TestMethod]
    public void ValidTDFlowchart()
    {
        //Arrange
        string direction = "LR";
        Flowchart flowchart = new(direction);
        string expected = @"flowchart LR
";

        //Act
        string result = flowchart.CalculateFlowchart(new(), new());

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
        Flowchart flowchart = new(direction);
        List<Node> nodes = new()
        {
            new("node1", "This is node 1")
        };
        string expected = @"flowchart LR
    node1[This is node 1]
";

        //Act
        string result = flowchart.CalculateFlowchart(nodes, new());

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
        Flowchart flowchart = new(direction);
        List<Node> nodes = new()
        {
            new("node1", "This is node 1"),
            new("node2", "This is node 2")
        };
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
";

        //Act
        string result = flowchart.CalculateFlowchart(nodes, new());

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
        Flowchart flowchart = new(direction);
        List<Node> nodes = new()
        {
            new("node1", "This is node 1"),
            new("node2", "This is node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", null)
        };
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1-->node2
";

        //Act
        string result = flowchart.CalculateFlowchart(nodes, links);

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
        Flowchart flowchart = new(direction);
        List<Node> nodes = new()
        {
            new("node1", "This is node 1"),
            new("node2", "This is node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "link text!")
        };
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1--link text!-->node2
";

        //Act
        string result = flowchart.CalculateFlowchart(nodes, links);

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }


}