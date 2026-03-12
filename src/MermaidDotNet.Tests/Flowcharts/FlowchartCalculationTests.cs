using MermaidDotNet.Diagrams;
using MermaidDotNet.Enums;
using MermaidDotNet.Models;

namespace MermaidDotNet.Tests.Flowcharts;

[TestClass]
public class FlowchartCalculationTests
{
    [TestMethod]
    public void ValidTDFlowchart()
    {
        //Arrange
        string direction = "LR";
        FlowchartDiagram flowchart = new(new(), new(), direction);
        string expected = @"flowchart LR";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is node 1")
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart LR
    node1[This is node 1]";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is node 1"),
            new("node2", "This is node 2")
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is node 1", ShapeType.Rounded),
            new("node2", "This is node 2", ShapeType.Rounded)
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart LR
    node1(This is node 1)
    node2(This is node 2)";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is node 1"),
            new("node2", "This is node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "")
        };
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1-->node2";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is node 1"),
            new("node2", "This is node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "link text!")
        };
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1--link text!-->node2";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is node 1"),
            new("node2", "This is node 2")
        };
        List<Link> links = new()
        {
            new Link("node1", "node2", "link text!", isBidirectional: true)
        };
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1<--link text!-->node2";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
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
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node3[This is node 3]
    node1--12s-->node2
    node1--3mins-->node3";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
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
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node3[This is node 3]
    node1--12s-->node2
    node1--3mins-->node3
    linkStyle 0 stroke-width:4px,stroke:red";

        //Act
        string result = flowchart.CalculateDiagram();

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
                new List<FlowNode>
                {
                    new("node1", "This is node 1"),
                    new("node2", "This is node 2")
                },
                new())
        };
        FlowchartDiagram flowchart = new(new(), new(), direction, subGraphs);
        string expected = @"flowchart LR
    subgraph graph1
    node1[This is node 1]
    node2[This is node 2]
    end";

        //Act
        string result = flowchart.CalculateDiagram();

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
                new List<FlowNode>
                {
                    new("node1", "This is node 1"),
                    new("node2", "This is node 2")
                },
                new()),
            new("graph2",
                new List<FlowNode>
                {
                    new("node3", "This is node 3"),
                    new("node4", "This is node 4")
                },
                new List<Link> {
                    new Link("node1", "node3", null)
                })
        };
        FlowchartDiagram flowchart = new(new(), new(), direction, subGraphs);
        string expected = @"flowchart LR
    subgraph graph1
    node1[This is node 1]
    node2[This is node 2]
    end
    subgraph graph2
    node3[This is node 3]
    node4[This is node 4]
    node1-->node3
    end";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
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
                new List<FlowNode>
                {
                    new("node1", "This is node 1"),
                    new("node2", "This is node 2")
                },
                new()),
            new("graph2",
                new List<FlowNode>
                {
                    new("node3", "This is node 3"),
                    new("node4", "This is node 4")
                },
                new List<Link> {
                    new Link("node1", "node3", null)
                })
        };
        FlowchartDiagram flowchart = new(nodes, links, direction, subGraphs);
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
    node5-->node6";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
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
                new List<FlowNode>
                {
                    new("node1", "This is node 1"),
                    new("node2", "This is node 2")
                },
                new(),
                direction),
            new("graph2",
                new List<FlowNode>
                {
                    new("node3", "This is node 3"),
                    new("node4", "This is node 4")
                },
                new List<Link> {
                    new Link("node1", "node3", null)
                },
                direction)
        };
        FlowchartDiagram flowchart = new(nodes, links, direction, subGraphs);
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
    node5-->node6";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is node 1", ShapeType.Rectangle),
            new("node2", "This is node 2", ShapeType.Rounded),
            new("node3", "This is node 3", ShapeType.Stadium),
            new("node4", "This is node 4", ShapeType.Cylinder),
            new("node5", "This is node 5", ShapeType.Circle),
            new("node6", "This is node 6", ShapeType.Rhombus),
            new("node7", "This is node 7", ShapeType.Hexagon)
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2(This is node 2)
    node3([This is node 3])
    node4[(This is node 4)]
    node5((This is node 5))
    node6{This is node 6}
    node7{{This is node 7}}";

        //Act
        string result = flowchart.CalculateDiagram();

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
                new List<FlowNode>
                {
                    new("node1", "This is node 1"),
                    new("node2", "This is node 2")
                },
                new(),
                "TB"),
            new("graph2",
                new List<FlowNode>
                {
                    new("node3", "This is node 3"),
                    new("node4", "This is node 4")
                },
                new List<Link> {
                    new Link("node1", "node3", null)
                },
                "BT")
        };
        FlowchartDiagram flowchart = new(new(), new(), direction, subGraphs);
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
    end";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
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
        FlowchartDiagram flowchart = new(nodes, links, direction);
        string expected = @"flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node3[This is node 3]
    node1--12s-->node2
    node1--3mins-->node3
    linkStyle 0 stroke-width:4px,stroke:red
    linkStyle 1 stroke-width:2px,stroke:blue";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is node 1")
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart BT
    node1[This is node 1]";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is node 1")
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart RL
    node1[This is node 1]";

        //Act
        string result = flowchart.CalculateDiagram();

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
        List<FlowNode> nodes = new()
        {
            new("node1", "This is node 1")
        };
        FlowchartDiagram flowchart = new(nodes, new(), direction);
        string expected = @"flowchart TB
    node1[This is node 1]";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }
}
