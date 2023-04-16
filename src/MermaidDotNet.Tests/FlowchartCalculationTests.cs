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
        string expected = "flowchart " + direction;

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
    node1[This is node1]
";

        //Act
        string result = flowchart.CalculateFlowchart(nodes, new());

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }


}