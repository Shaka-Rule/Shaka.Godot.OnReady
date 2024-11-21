using Godot;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


namespace Shaka.Godot.OnReady.Tests;

public class AnalyzerTests
{
    [Fact]
    public Task MembersAreNotNodesShowsSgor001Error()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              namespace TestNamespace;

                              public partial class Examples : Godot.Node
                              {
                                  [OnReady("frezfrezf")]
                                  public partial List<MyTest> OnReadyList { get; }
                              
                                  [OnReady("/root/mynode")]
                                  public partial int OnReadyInt();
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.SettingsIgnoreInitGenerated);
    }
    
    [Fact]
    public Task MembersAreNotPartialShowsSgor002Error()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              using Godot;
                              namespace TestNamespace;

                              public partial class Examples : Godot.Node
                              {
                                  [OnReady("frezfrezf")]
                                  public Godot.Node OnReadyList { get; }
                              
                                  [OnReady("/root/mynode")]
                                  public Godot.Node OnReadyInt();
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.SettingsIgnoreInitGenerated);
    }
    
    [Fact]
    public Task MembersAreStaticShowsSgor003Error()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              using Godot;
                              namespace TestNamespace;

                              public partial class Examples : Godot.Node
                              {
                                  [OnReady("frezfrezf")]
                                  public static partial Godot.Node OnReadyList { get; }
                              
                                  [OnReady("/root/mynode")]
                                  public static partial Godot.Node OnReadyInt();
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.SettingsIgnoreInitGenerated);
    }
    
    [Fact]
    public Task MembersAreStaticNonNodeNonPartialShowsSgor001_002_003Error()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              using Godot;
                              namespace TestNamespace;

                              public partial class Examples : Godot.Node
                              {
                                  [OnReady("frezfrezf")]
                                  public static int OnReadyList { get; }
                              
                                  [OnReady("/root/mynode")]
                                  public static List<int> OnReadyInt();
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.SettingsIgnoreInitGenerated);
    }
    
    [Fact]
    public Task MemberIsValidAndOtherIsStaticNonNodeNonPartialShowsSgor001_002_003Error()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              using Godot;
                              namespace TestNamespace;

                              public partial class Examples : Godot.Node
                              {
                                  [OnReady("frezfrezf")]
                                  public static int OnReadyIntInvalid { get; }
                                  [OnReady("/root/mynode")]
                                  public static List<int> OnReadyListIntInvalid();
                                  
                                  [OnReady("frezfrezf")]
                                  public partial Godot.Node2D OnReadyNode2DValid { get; }
                                  [OnReady("/root/mynode")]
                                  public partial Godot.Node3D OnReadyNode3DValid();
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.SettingsIgnoreInitGenerated);
    }
    
    [Fact]
    public Task ClassDoesNotDeriveFromNodeShowsSgor004()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              namespace TestNamespace;

                              public partial class Examples
                              {
                                  [OnReady("Player")]
                                  public partial Godot.Node2D Player { get; }
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.SettingsIgnoreInitGenerated);
    }
}