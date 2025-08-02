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
    public void TwoNodesRoundedNodesFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "This is node 1", Node.ShapeType.Rounded),
            new("node2", "This is node 2", Node.ShapeType.Rounded)
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart LR
    node1(This is node 1)
    node2(This is node 2)
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
            new Link("node1", "node2", "")
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
    public void TwoNodesAndBidirectionalLinkTextFlowchart()
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
            new Link("node1", "node2", "link text!", isBidirectional: true)
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1<--link text!-->node2
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

    [TestMethod]
    public void ThreeNodesAndTwoLinksWithMultipleColorsFlowchart()
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
            new Link("node1", "node2", "12s", "stroke-width:4px,stroke:red"),
            new Link("node1", "node3", "3mins")
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node3[This is node 3]
    node1--12s-->node2
    node1--3mins-->node3
    linkStyle 0 stroke-width:4px,stroke:red
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void TwoNodesInASubGraphFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<SubGraph> subGraphs = new()
        {
            new("graph1",
                new List<Node>
                {
                    new("node1", "This is node 1"),
                    new("node2", "This is node 2")
                },
                new())
        };
        Flowchart flowchart = new(direction, new(), new(), subGraphs);
        string expected = @"flowchart LR
    subgraph graph1
    node1[This is node 1]
    node2[This is node 2]
    end
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void FourNodesInTwoSubGraphsFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<SubGraph> subGraphs = new()
        {
            new("graph1",
                new List<Node>
                {
                    new("node1", "This is node 1"),
                    new("node2", "This is node 2")
                },
                new()),
            new("graph2",
                new List<Node>
                {
                    new("node3", "This is node 3"),
                    new("node4", "This is node 4")
                },
                new List<Link> {
                    new Link("node1", "node3", null)
                })
        };
        Flowchart flowchart = new(direction, new(), new(), subGraphs);
        string expected = @"flowchart LR
    subgraph graph1
    node1[This is node 1]
    node2[This is node 2]
    end
    subgraph graph2
    node3[This is node 3]
    node4[This is node 4]
    node1-->node3
    end
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void FourNodesInTwoSubGraphsAndTwoSingleNodesFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node5", "This is node 5"),
            new("node6", "This is node 6"),
        };
        List<Link> links = new()
        {
            new Link("node5", "node6")
        };
        List<SubGraph> subGraphs = new()
        {
            new("graph1",
                new List<Node>
                {
                    new("node1", "This is node 1"),
                    new("node2", "This is node 2")
                },
                new()),
            new("graph2",
                new List<Node>
                {
                    new("node3", "This is node 3"),
                    new("node4", "This is node 4")
                },
                new List<Link> {
                    new Link("node1", "node3", null)
                })
        };
        Flowchart flowchart = new(direction, nodes, links, subGraphs);
        string expected = @"flowchart LR
    subgraph graph1
    node1[This is node 1]
    node2[This is node 2]
    end
    subgraph graph2
    node3[This is node 3]
    node4[This is node 4]
    node1-->node3
    end
    node5[This is node 5]
    node6[This is node 6]
    node5-->node6
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void FourNodesInTwoSubGraphsWithDirectionAndTwoSingleNodesFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node5", "This is node 5"),
            new("node6", "This is node 6"),
        };
        List<Link> links = new()
        {
            new Link("node5", "node6")
        };
        List<SubGraph> subGraphs = new()
        {
            new("graph1",
                new List<Node>
                {
                    new("node1", "This is node 1"),
                    new("node2", "This is node 2")
                },
                new(),
                direction),
            new("graph2",
                new List<Node>
                {
                    new("node3", "This is node 3"),
                    new("node4", "This is node 4")
                },
                new List<Link> {
                    new Link("node1", "node3", null)
                },
                direction)
        };
        Flowchart flowchart = new(direction, nodes, links, subGraphs);
        string expected = @"flowchart LR
    subgraph graph1
    direction LR
    node1[This is node 1]
    node2[This is node 2]
    end
    subgraph graph2
    direction LR
    node3[This is node 3]
    node4[This is node 4]
    node1-->node3
    end
    node5[This is node 5]
    node6[This is node 6]
    node5-->node6
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void DifferentNodeShapesFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<Node> nodes = new()
        {
            new("node1", "This is node 1", Node.ShapeType.Rectangle),
            new("node2", "This is node 2", Node.ShapeType.Rounded),
            new("node3", "This is node 3", Node.ShapeType.Stadium),
            new("node4", "This is node 4", Node.ShapeType.Cylinder),
            new("node5", "This is node 5", Node.ShapeType.Circle),
            new("node6", "This is node 6", Node.ShapeType.Rhombus),
            new("node7", "This is node 7", Node.ShapeType.Hexagon)
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart LR
    node1[This is node 1]
    node2(This is node 2)
    node3([This is node 3])
    node4[(This is node 4)]
    node5((This is node 5))
    node6{This is node 6}
    node7{{This is node 7}}
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void SubgraphsWithDirectionsFlowchart()
    {
        //Arrange
        string direction = "LR";
        List<SubGraph> subGraphs = new()
        {
            new("graph1",
                new List<Node>
                {
                    new("node1", "This is node 1"),
                    new("node2", "This is node 2")
                },
                new(),
                "TB"),
            new("graph2",
                new List<Node>
                {
                    new("node3", "This is node 3"),
                    new("node4", "This is node 4")
                },
                new List<Link> {
                    new Link("node1", "node3", null)
                },
                "BT")
        };
        Flowchart flowchart = new(direction, new(), new(), subGraphs);
        string expected = @"flowchart LR
    subgraph graph1
    direction TB
    node1[This is node 1]
    node2[This is node 2]
    end
    subgraph graph2
    direction BT
    node3[This is node 3]
    node4[This is node 4]
    node1-->node3
    end
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void LinkStylesFlowchart()
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
            new Link("node1", "node2", "12s", "stroke-width:4px,stroke:red"),
            new Link("node1", "node3", "3mins", "stroke-width:2px,stroke:blue")
        };
        Flowchart flowchart = new(direction, nodes, links);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node3[This is node 3]
    node1--12s-->node2
    node1--3mins-->node3
    linkStyle 0 stroke-width:4px,stroke:red
    linkStyle 1 stroke-width:2px,stroke:blue
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }

    [TestMethod]
    public void ValidBTDirectionFlowchart()
    {
        //Arrange
        string direction = "BT";
        List<Node> nodes = new()
        {
            new("node1", "This is node 1")
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart BT
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
    public void ValidRLDirectionFlowchart()
    {
        //Arrange
        string direction = "RL";
        List<Node> nodes = new()
        {
            new("node1", "This is node 1")
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart RL
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
    public void ValidTBDirectionFlowchart()
    {
        //Arrange
        string direction = "TB";
        List<Node> nodes = new()
        {
            new("node1", "This is node 1")
        };
        Flowchart flowchart = new(direction, nodes, new());
        string expected = @"flowchart TB
    node1[This is node 1]
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }
}
