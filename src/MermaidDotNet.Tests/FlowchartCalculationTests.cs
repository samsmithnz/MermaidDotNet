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


}