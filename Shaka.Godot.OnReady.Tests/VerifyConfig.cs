using System.Runtime.CompilerServices;

namespace Shaka.Godot.OnReady.Tests;

public static class VerifyConfig
{
    [ModuleInitializer]
    public static void Init() =>
        VerifySourceGenerators.Initialize();
}