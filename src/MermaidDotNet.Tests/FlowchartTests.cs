namespace MermaidDotNet.Tests;

[TestClass]
public class FlowchartTests
{
    [TestMethod]
    public void ValidTDFlowchart()
    {
        //Arrange
        Flowchart flowchart = new("TD", new(), new());

        //Act

        //Assert
        Assert.IsNotNull(flowchart);
    }

    [TestMethod]
    public void ValidLRFlowchart()
    {
        //Arrange
        Flowchart flowchart = new("LR", new(), new());

        //Act

        //Assert
        Assert.IsNotNull(flowchart);
    }

    [TestMethod]
    public void InvalidNoDirectionFlowchart()
    {
        //Arrange
        try
        {
            Flowchart flowchart = new("none", new(), new());

            //Act

            //Assert
            Assert.IsNotNull(flowchart);
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Direction none is currently unsupported", ex.Message);
        }
    }
}