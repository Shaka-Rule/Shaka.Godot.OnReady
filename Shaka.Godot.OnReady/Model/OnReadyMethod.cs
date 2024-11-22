using Microsoft.CodeAnalysis;

namespace Shaka.Godot.OnReady;

internal record OnReadyMethod(string Name, Accessibility Accessibility, string Type, bool Nullable, string NodePath) : OnReadyMember(Name, Accessibility, Type, Nullable, NodePath)
{
    public override string ToSource()
    {
        var onReadyMember =
            $$"""
              {{PrivateMember}}
              {{AccessModifier}} partial {{Type}}{{NullableModifier}} {{Name}}()
              {
                  return {{PrivateName}} ??= {{NodeStatement}};
              }
              """;
        return onReadyMember;
    }
}