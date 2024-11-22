using Microsoft.CodeAnalysis;

namespace Shaka.Godot.OnReady;

internal record OnReadyProperty(string Name, Accessibility Accessibility, string Type, bool Nullable, string NodePath) : OnReadyMember(Name, Accessibility, Type, Nullable, NodePath)
{
    public override string ToSource()
    {
        return $"""
                {PrivateMember}
                {AccessModifier} partial {Type} {Name} => {PrivateName} ??= {NodeStatement};
                """;
    }
}