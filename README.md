# MermaidDotNet
[![CI/CD](https://github.com/samsmithnz/MermaidDotNet/actions/workflows/workflow.yml/badge.svg)](https://github.com/samsmithnz/MermaidDotNet/actions/workflows/workflow.yml)
[![Latest NuGet package](https://img.shields.io/nuget/v/MermaidDotNet)](https://www.nuget.org/packages/MermaidDotNet/)
[![Current Release](https://img.shields.io/github/release/samsmithnz/MermaidDotNet/all.svg)](https://github.com/samsmithnz/MermaidDotNet/releases)

A .NET wrapper to create [Mermaid](https://mermaid.js.org/) code to render flowcharts, that can then be inserted into markdown or directly displayed in HTML with mermaid.js.

Very simple example, to create a Left->Right graph (LR), with two nodes linked. 
```csharp
    string direction = "LR";
    List<Node> nodes = new()
    {
        new("node1", "This is node 1"),
        new("node2", "This is node 2", Node.ShapeType.Hexagon),
        new("node3", "This is node 3", Node.ShapeType.Rounded)
    };
    List<Link> links = new()
    {
        new Link("node1", "node2", "12s"),
        new Link("node1", "node3", "3mins")
    };
    Flowchart flowchart = new(direction, nodes, links);
    string result = flowchart.CalculateFlowchart();
```

The mermaid result is below - which can be inserted into markdown in GitHub ([blog announcement](https://github.blog/2022-02-14-include-diagrams-markdown-files-mermaid/))

```
flowchart LR
    node1[This is node 1]
    node2{{This is node 2}}
    node3(This is node 3)
    node1--12s-->node2
    node1--3mins-->node3
```

Which when rendered in mermaid, looks like this:
```mermaid  
flowchart LR
    node1[This is node 1]
    node2{{This is node 2}}
    node3(This is node 3)
    node1--12s-->node2
    node1--3mins-->node3
```

It's also possible to insert into HTML and rendor on the web. Here is a sample, referencing the mermaid.js CDN.

```html
<h2>Production Graph</h2>
<body>
    Here is a mermaid diagram:
    <pre class="mermaid">
flowchart LR
    node1[This is node 1]
    node2{{This is node 2}}
    node3(This is node 3)
    node1--12s-->node2
    node1--3mins-->node3
    </pre>
    <script type="module">
        import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.esm.min.mjs';
        mermaid.initialize({ startOnLoad: true });
    </script>
</body>
```

## Class Diagrams

This library also supports creating class diagrams. Below is an example of how to create a class diagram with classes, attributes, methods, and relationships.

```csharp
    List<Class> classes = new()
    {
        new Class("Class1", new List<Attribute>(), new List<Method>()),
        new Class("Class2", new List<Attribute>
        {
            new Attribute("Attribute1", "string"),
            new Attribute("Attribute2", "int")
        }, new List<Method>
        {
            new Method("Method1", "void", new List<string> { "param1", "param2" }),
            new Method("Method2", "int", new List<string> { "param1" })
        })
    };
    List<Relationship> relationships = new()
    {
        new Relationship("Class1", "Class2", "<|--")
    };
    ClassDiagram classDiagram = new(classes, relationships);
    string result = classDiagram.CalculateClassDiagram();
```

The mermaid result is below - which can be inserted into markdown in GitHub.

```
classDiagram
    class Class1 {
    }
    class Class2 {
        string Attribute1
        int Attribute2
        void Method1(param1, param2)
        int Method2(param1)
    }
    Class1 <|-- Class2
```

Which when rendered in mermaid, looks like this:
```mermaid
classDiagram
    class Class1 {
    }
    class Class2 {
        string Attribute1
        int Attribute2
        void Method1(param1, param2)
        int Method2(param1)
    }
    Class1 <|-- Class2
```

It's also possible to insert into HTML and render on the web. Here is a sample, referencing the mermaid.js CDN.

```html
<h2>Class Diagram</h2>
<body>
    Here is a mermaid class diagram:
    <pre class="mermaid">
classDiagram
    class Class1 {
    }
    class Class2 {
        string Attribute1
        int Attribute2
        void Method1(param1, param2)
        int Method2(param1)
    }
    Class1 <|-- Class2
    </pre>
    <script type="module">
        import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.esm.min.mjs';
        mermaid.initialize({ startOnLoad: true });
    </script>
</body>
