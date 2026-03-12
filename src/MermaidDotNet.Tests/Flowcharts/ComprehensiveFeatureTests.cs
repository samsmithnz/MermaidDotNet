using MermaidDotNet.Diagrams;
using MermaidDotNet.Enums;
using MermaidDotNet.Models;

namespace MermaidDotNet.Tests.Flowcharts;

/// <summary>
/// Comprehensive test to demonstrate all the new flowchart functionality added
/// </summary>
[TestClass]
public class ComprehensiveFeatureTests
{
    [TestMethod]
    public void AllNewFeaturesShowcaseFlowchart()
    {
        //Arrange - Create a flowchart that uses every new feature
        string direction = "BT"; // New direction support
        List<FlowNode> nodes = new()
        {
            // Various new node shapes with styling and click actions
            new("start", "Start Process", ShapeType.Circle, "startNode", "startFlow()"),
            new("input", "Input Data", ShapeType.Parallelogram, "inputClass"),
            new("validate", "Validate?", ShapeType.Rhombus),
            new("process", "Process", ShapeType.Subroutine, null, "processData()"),
            new("store", "Store Result", ShapeType.Cylinder),
            new("finish", "Complete", ShapeType.Stadium)
        };

        List<Link> links = new()
        {
            // Various new link types and arrow types
            new Link("start", "input", "begin", null, false, LinkType.Normal),
            new Link("input", "validate", "check", null, false, LinkType.Dotted),
            new Link("validate", "process", "valid", "stroke:green,stroke-width:3px", false, LinkType.Thick),
            new Link("process", "store", "", null, false, LinkType.Normal, ArrowType.Circle),
            new Link("store", "finish", "", null, false, LinkType.Invisible),
            new Link("validate", "input", "invalid", null, true, LinkType.Normal, ArrowType.Cross)
        };

        FlowchartDiagram flowchart = new(nodes, links, direction);

        string expected = @"flowchart BT
    start((Start Process))
    input[/Input Data/]
    validate{Validate?}
    process[[Process]]
    store[(Store Result)]
    finish([Complete])
    start--begin-->input
    input-.check.->validate
    validate==valid==>process
    process--ostore
    store~~~>finish
    validatex--invalid--xinput
    linkStyle 0 stroke:green,stroke-width:3px
    class start startNode
    class input inputClass
    click start ""startFlow()""
    click process ""processData()""";

        //Act
        string result = flowchart.CalculateDiagram();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }
}