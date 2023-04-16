# MermaidDotNet
[![CI/CD](https://github.com/samsmithnz/MermaidDotNet/actions/workflows/workflow.yml/badge.svg)](https://github.com/samsmithnz/MermaidDotNet/actions/workflows/workflow.yml)

Very simple example, to create a Left->Right graph (LR), with two nodes linked. 
```csharp
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
    string result = flowchart.CalculateFlowchart(nodes, links);
```

The mermaid result is:
```mermaid  
flowchart LR
    node1[This is node 1]
    node2[This is node 2]
    node1--link text!-->node2
```