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



    [TestMethod]
    public void NodesAddedIncorrectlyFlowchart()
    {
        //Arrange
        try
        {
            Flowchart flowchart = new("LR", new(), new());
            flowchart.Nodes.Add(new("node1", "node1"));

            //Act

            //Assert
            Assert.IsNotNull(flowchart);
        }
        catch (Exception ex)
        {
            Assert.AreEqual("The NavigationNodes collection is empty, but Nodes collection is not empty. This is likely an issue because Nodes were added manually instead of as a collection in the FlowChart constructor", ex.Message);
        }
    }
}