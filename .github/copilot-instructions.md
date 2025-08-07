# MermaidDotNet

MermaidDotNet is a comprehensive .NET wrapper library for creating Mermaid flowcharts with full syntax support. The solution includes a core .NET 8 library, unit tests, and sample applications (MVC Web and Blazor) demonstrating usage.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

### Prerequisites
Install the required .NET SDKs in this exact order:
- `.NET 8.0 SDK` - Required for core library and tests
- `.NET 9.0 SDK` - Required for sample applications

```bash
# Install .NET 8 first (required for tests)
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 8.0

# Install .NET 9 (for sample applications)
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 9.0

export PATH="/home/runner/.dotnet:$PATH"
```

### Bootstrap, Build, and Test
Run these commands in this exact sequence:

```bash
cd /home/runner/work/MermaidDotNet/MermaidDotNet
export PATH="/home/runner/.dotnet:$PATH"

# Restore packages - takes 2-3 seconds. NEVER CANCEL. Set timeout to 5+ minutes.
dotnet restore src/MermaidDotNet.sln

# Build solution - takes 5-6 seconds. NEVER CANCEL. Set timeout to 5+ minutes.
dotnet build src/MermaidDotNet.sln -c Release

# Run tests - takes 3-4 seconds. NEVER CANCEL. Set timeout to 2+ minutes.
dotnet test src/MermaidDotNet.Tests/MermaidDotNet.Tests.csproj -c Release
```

**CRITICAL**: NEVER CANCEL build or test commands. Builds may take 15-20 seconds, tests take ~4 seconds. Always use timeouts of 5+ minutes for builds and 2+ minutes for tests.

### Core Library Only (Faster)
For development focused on the core library without sample apps:

```bash
# Core library restore - takes 1 second
dotnet restore src/MermaidDotNet/MermaidDotNet.csproj

# Core library build - takes 2 seconds 
dotnet build src/MermaidDotNet/MermaidDotNet.csproj -c Release

# Run tests - takes 3-4 seconds
dotnet test src/MermaidDotNet.Tests/MermaidDotNet.Tests.csproj -c Release
```

### Run Sample Applications
Both sample applications demonstrate the library in action:

**MVC Web Application:**
```bash
cd src/MermaidDotNet.MVCWeb
export PATH="/home/runner/.dotnet:$PATH"
dotnet run --urls=http://localhost:5000
# Access at http://localhost:5000
```

**Blazor WebAssembly Application:**
```bash
cd src/MermaidDotNet.BlazorApp  
export PATH="/home/runner/.dotnet:$PATH"
dotnet run --urls=http://localhost:5001
# Access at http://localhost:5001
```

### NuGet Package Creation
```bash
# Create NuGet package - takes 1-2 seconds
dotnet pack src/MermaidDotNet/MermaidDotNet.csproj -c Release
# Output: src/MermaidDotNet/bin/Release/MermaidDotNet.{version}.nupkg
```

## Validation

### Always Test After Changes
Run this complete validation sequence after making any code changes:

1. **Build Validation:**
   ```bash
   dotnet build src/MermaidDotNet.sln -c Release
   # Expected: Success with XML documentation warnings (58 warnings normal)
   ```

2. **Test Validation:**
   ```bash
   dotnet test src/MermaidDotNet.Tests/MermaidDotNet.Tests.csproj -c Release
   # Expected: 43 tests pass, 0 failures
   ```

3. **Sample Application Validation:**
   ```bash
   # Start MVC app
   cd src/MermaidDotNet.MVCWeb && dotnet run --urls=http://localhost:5000 &
   
   # Test response
   curl -I http://localhost:5000
   # Expected: HTTP/1.1 200 OK
   
   # Start Blazor app  
   cd ../MermaidDotNet.BlazorApp && dotnet run --urls=http://localhost:5001 &
   
   # Test response
   curl -I http://localhost:5001  
   # Expected: HTTP/1.1 200 OK
   ```

4. **Manual Functional Testing:**
   - ALWAYS manually test flowchart generation by creating a simple test
   - Verify Mermaid syntax output matches expected format
   - Test both basic and advanced node types/link types

### Example Manual Test
Create this test to validate core functionality works correctly:
```csharp
using MermaidDotNet.Models;

[TestMethod]
public void BasicFlowchartValidation()
{
    // Arrange - Create a simple flowchart
    var nodes = new List<Node>
    {
        new("start", "Start", Node.ShapeType.Circle),
        new("process", "Process Data", Node.ShapeType.Rectangle),
        new("end", "End", Node.ShapeType.Stadium)
    };
    var links = new List<Link>
    {
        new Link("start", "process", "begin"),
        new Link("process", "end", "complete")
    };
    var flowchart = new Flowchart("LR", nodes, links);
    
    // Act - Generate Mermaid syntax
    string result = flowchart.CalculateFlowchart();
    
    // Assert - Verify valid Mermaid syntax output
    Assert.IsTrue(result.Contains("flowchart LR"));
    Assert.IsTrue(result.Contains("start((Start))"));
    Assert.IsTrue(result.Contains("process[Process Data]"));
    Assert.IsTrue(result.Contains("end([End])"));
    Assert.IsTrue(result.Contains("start--begin-->process"));
    Assert.IsTrue(result.Contains("process--complete-->end"));
}
```

## Common Tasks

### Repository Structure
```
MermaidDotNet/
├── src/
│   ├── MermaidDotNet/              # Core library (.NET 8)
│   ├── MermaidDotNet.Tests/        # Unit tests (.NET 8)  
│   ├── MermaidDotNet.MVCWeb/       # Sample MVC app (.NET 9)
│   ├── MermaidDotNet.BlazorApp/    # Sample Blazor app (.NET 9)
│   └── MermaidDotNet.sln           # Solution file
├── .github/workflows/workflow.yml  # CI/CD pipeline
├── GitVersion.yml                  # Version configuration
└── README.md                       # Documentation
```

### Key Project Files
- **Core Library**: `src/MermaidDotNet/MermaidDotNet.csproj`
- **Tests**: `src/MermaidDotNet.Tests/MermaidDotNet.Tests.csproj`  
- **Solution**: `src/MermaidDotNet.sln`

### Common Scenarios

**Adding New Node Types:**
1. Update `Node.cs` ShapeType enum
2. Update `Node.OpenShape()` and `Node.CloseShape()` methods  
3. Add unit tests in `MermaidDotNet.Tests`
4. Run full validation sequence

**Adding New Link Types:**
1. Update `Link.cs` LinkType enum
2. Update flowchart generation logic in `Flowchart.cs`
3. Add unit tests
4. Run full validation sequence

**Testing Sample Applications:**
- Both applications include working examples of the library
- MVC app demonstrates server-side rendering
- Blazor app demonstrates client-side usage
- Always test both after core library changes

## Build Warnings Expected
The build produces 58-60 XML documentation warnings - this is normal and expected. The library builds successfully despite these warnings.

## CI/CD Integration
The project uses GitHub Actions for:
- Automated building and testing
- Code coverage via Coveralls
- SonarCloud analysis  
- NuGet package publishing
- Automatic releases via GitVersion

Always ensure your changes pass local validation before pushing to avoid CI failures.