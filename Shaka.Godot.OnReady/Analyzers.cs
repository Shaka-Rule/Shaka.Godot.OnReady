using Microsoft.CodeAnalysis;

namespace Shaka.Godot.OnReady;

public static class Analyzers
{
    private const string NodeClass = "Godot.Node";
    
    public static readonly DiagnosticDescriptor OnReadyMemberReturnMustDeriveFromNode =
        new(id: "SGOR001",
            title: $"On ready member type must derive from {NodeClass}",
            messageFormat: $"The [OnReady] member '{{0}}' type must derive from {NodeClass}",
            category: "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            $"The [OnReady] member type must derive from {NodeClass}. Change the type to a type which derives from Node.");

    public static readonly DiagnosticDescriptor OnReadyMemberMustBeEmptyPartial =
        new(id: "SGOR002",
            title: "On ready member must be partial and must not be implemented",
            messageFormat: "The [OnReady] member '{0}' must be partial without an implementation",
            category: "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            "The [OnReady] member must be partial and must not have a body. Make the member partial and remove the body.");

    public static readonly DiagnosticDescriptor OnReadyMemberCannotBeStatic =
        new(id: "SGOR003",
            title: "On ready member cannot be static",
            messageFormat: "The [OnReady] member '{0}' cannot be static",
            category: "Usage",
            DiagnosticSeverity.Error,
            isEnabledByDefault: true,
            "The [OnReady] member cannot be static. Remove the static keyword from the member.");
}