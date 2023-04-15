namespace MermaidDotNet.Tests;

[TestClass]
public class FlowchartTests
{
    [TestMethod]
    public void ValidTDFlowchart()
    {
        //Arrange
        Flowchart flowchart = new("TD");

        //Act

        //Assert
        Assert.IsNotNull(flowchart);
    }

    [TestMethod]
    public void ValidLRFlowchart()
    {
        //Arrange
        Flowchart flowchart = new("LR");

        //Act

        //Assert
        Assert.IsNotNull(flowchart);
    }

    [TestMethod]
    public void ValidNoDirectionFlowchart()
    {
        //Arrange
        try
        {
            Flowchart flowchart = new("none");

            //Act

            //Assert
            Assert.IsNotNull(flowchart);
        }
        catch (Exception ex)
        {
            Assert.AreEqual("Direction none is currently unsupported",ex.Message  );
        }
    }
}