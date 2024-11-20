using System.Runtime.CompilerServices;
using Godot;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Shaka.Godot.OnReady.Tests;

public static class VerifyConfig
{
    [ModuleInitializer]
    public static void Init()
    {
        VerifySourceGenerators.Initialize();
    }
    internal static VerifySettings Settings => GetSettings();
    private static VerifySettings GetSettings()
    {
        var settings = new VerifySettings();
        settings.UseDirectory("VerifyResults");
        settings.IgnoreGeneratedResult(r => r.HintName.Contains("OnReadyAttribute.g.cs"));
        return settings;
    }
    
    internal static GeneratorDriver BuildDriver(params IEnumerable<SyntaxTree> inputSources)
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