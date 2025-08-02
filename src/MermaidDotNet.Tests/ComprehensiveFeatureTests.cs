using MermaidDotNet.Models;

namespace MermaidDotNet.Tests;

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
        List<Node> nodes = new()
        {
            // Various new node shapes with styling and click actions
            new("start", "Start Process", Node.ShapeType.Circle, "startNode", "startFlow()"),
            new("input", "Input Data", Node.ShapeType.Parallelogram, "inputClass"),
            new("validate", "Validate?", Node.ShapeType.Rhombus),
            new("process", "Process", Node.ShapeType.Subroutine, null, "processData()"),
            new("store", "Store Result", Node.ShapeType.Cylinder),
            new("finish", "Complete", Node.ShapeType.Stadium)
        };
        
        List<Link> links = new()
        {
            // Various new link types and arrow types
            new Link("start", "input", "begin", null, false, Link.LinkType.Normal),
            new Link("input", "validate", "check", null, false, Link.LinkType.Dotted),
            new Link("validate", "process", "valid", "stroke:green,stroke-width:3px", false, Link.LinkType.Thick),
            new Link("process", "store", "", null, false, Link.LinkType.Normal, Link.ArrowType.Circle),
            new Link("store", "finish", "", null, false, Link.LinkType.Invisible),
            new Link("validate", "input", "invalid", null, true, Link.LinkType.Normal, Link.ArrowType.Cross)
        };

        Flowchart flowchart = new(direction, nodes, links);
        
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
    click process ""processData()""
";

        //Act
        string result = flowchart.CalculateFlowchart();

        //Assert
        Assert.IsNotNull(flowchart);
        Assert.IsNotNull(result);
        Assert.AreEqual(expected, result);
    }
}