using Godot;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


namespace Shaka.Godot.OnReady.Tests;

public class AnalyzerTests
{
    private static VerifySettings Settings => GetSettings();
    private static VerifySettings GetSettings()
    {
        var settings = new VerifySettings();
        settings.UseDirectory("VerifyResults");
        settings.IgnoreGeneratedResult(r => r.HintName.Contains("OnReadyAttribute.g.cs"));
        return settings;
    }
    
    [Fact]
    public Task MembersAreNotNodesShowsSgor001Error()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              namespace TestNamespace;

                              public class Examples
                              {
                                  [OnReady("frezfrezf")]
                                  public partial List<MyTest> OnReadyList { get; set; }
                              
                                  [OnReady("/root/mynode")]
                                  public partial int OnReadyInt();
                              }
                              """;
        var driver = BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, Settings);
    }
    
    [Fact]
    public Task MembersAreNotPartialShowsSgor002Error()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              using Godot;
                              namespace TestNamespace;

                              public class Examples
                              {
                                  [OnReady("frezfrezf")]
                                  public Godot.Node OnReadyList { get; set; }
                              
                                  [OnReady("/root/mynode")]
                                  public Godot.Node OnReadyInt();
                              }
                              """;
        var driver = BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, Settings);
    }
    
    [Fact]
    public Task MembersAreStaticShowsSgor003Error()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              using Godot;
                              namespace TestNamespace;

                              public class Examples
                              {
                                  [OnReady("frezfrezf")]
                                  public static partial Godot.Node OnReadyList { get; set; }
                              
                                  [OnReady("/root/mynode")]
                                  public static partial Godot.Node OnReadyInt();
                              }
                              """;
        var driver = BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, Settings);
    }
    
    [Fact]
    public Task MembersAreStaticNonNodeNonPartialShowsSgor001_002_003Error()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              using Godot;
                              namespace TestNamespace;

                              public class Examples
                              {
                                  [OnReady("frezfrezf")]
                                  public static int OnReadyList { get; set; }
                              
                                  [OnReady("/root/mynode")]
                                  public static List<int> OnReadyInt();
                              }
                              """;
        var driver = BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, Settings);
    }
    
    [Fact]
    public Task MemberIsValidAndOtherIsStaticNonNodeNonPartialShowsSgor001_002_003Error()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              using Godot;
                              namespace TestNamespace;

                              public class Examples
                              {
                                  [OnReady("frezfrezf")]
                                  public static int OnReadyIntInvalid { get; set; }
                                  [OnReady("/root/mynode")]
                                  public static List<int> OnReadyListIntInvalid();
                                  
                                  [OnReady("frezfrezf")]
                                  public partial Godot.Node2D OnReadyNode2DValid { get; set; }
                                  [OnReady("/root/mynode")]
                                  public partial Godot.Node3D OnReadyNode3DValid();
                              }
                              """;
        var driver = BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, Settings);
    }

    private static GeneratorDriver BuildDriver(params IEnumerable<SyntaxTree> inputSources)
    {
        var compilation = CSharpCompilation.Create(nameof(AnalyzerTests),
            inputSources,
            [
                // To support 'System.Attribute' inheritance, add reference to 'System.Private.CoreLib'.
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Node).Assembly.Location)
            ]);
        var generator = new OnReadyGenerator();

        var driver = CSharpGeneratorDriver.Create(generator);
        return driver.RunGenerators(compilation);
    }
}