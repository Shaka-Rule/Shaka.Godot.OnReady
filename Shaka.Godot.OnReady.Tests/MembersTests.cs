using Microsoft.CodeAnalysis.CSharp;

namespace Shaka.Godot.OnReady.Tests;

public class MembersTests
{
    [Fact]
    public Task MemberGeneratedFor1Property()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              namespace TestNamespace;

                              public partial class Examples : Godot.Node3D
                              {
                                  [OnReady("Player")]
                                  public partial Godot.Node2D Player { get; }
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.Settings);
    }
    
    [Fact]
    public Task MembersGeneratedFor2Properties()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              namespace TestNamespace;

                              public partial class Examples : Godot.Node2D
                              {
                                  [OnReady("Player")]
                                  public partial Godot.Node2D Player { get; }
                                  [OnReady("Player/Sprite")]
                                  public partial Godot.Sprite2D Sprite { get; }
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.Settings);
    }
    
    [Fact]
    public Task MemberGeneratedFor1Method()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              namespace TestNamespace;

                              public partial class Examples : Godot.Node2D
                              {
                                  [OnReady("Player")]
                                  public partial Godot.Node2D Player();
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.Settings);
    }
    
    [Fact]
    public Task MembersGeneratedFor2Methods()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              namespace TestNamespace;

                              public partial class Examples : Godot.Node2D
                              {
                                  [OnReady("Player")]
                                  public partial Godot.Node2D Player();
                                  [OnReady("Player/Sprite")]
                                  public partial Godot.Sprite2D Sprite();
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.Settings);
    }
    
    [Fact]
    public Task MembersGeneratedFor1PropertyAnd1Method()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              namespace TestNamespace;

                              public partial class Examples : Godot.Node2D
                              {
                                  [OnReady("Player")]
                                  public partial Godot.Node2D Player { get; }
                                  [OnReady("Player/Sprite")]
                                  public partial Godot.Sprite2D Sprite();
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.Settings);
    }
    
    [Fact]
    public Task MembersGeneratedFor1MethodAnd1Property()
    {
        const string source = """
                              using Shaka.Godot.OnReady;
                              namespace TestNamespace;

                              public partial class Examples : Godot.Node2D
                              {
                                  [OnReady("Player")]
                                  public partial Godot.Node2D Player();
                                  [OnReady("Player/Sprite")]
                                  public partial Godot.Sprite2D Sprite { get; }
                              }
                              """;
        var driver = VerifyConfig.BuildDriver(CSharpSyntaxTree.ParseText(source));

        return Verify(driver, VerifyConfig.Settings);
    }
}