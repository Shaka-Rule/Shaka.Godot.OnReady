using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Shaka.Godot.OnReady;

internal abstract record OnReadyMember(string Name, Accessibility Accessibility, string Type, bool Nullable, string NodePath) : ISourceText

{
    public abstract string ToSource();
    protected string AccessModifier => SyntaxFacts.GetText(Accessibility);
    protected string PrivateName
    {
        get
        {
            if (string.IsNullOrWhiteSpace(field))
            {
                field = Name.ToUnderScoreFirstLower();
            }

            return field;
        }
    } = string.Empty;
    protected string PrivateMember => $$"""
                                        private {{Type}}? {{PrivateName}};
                                        """;
    protected string NodeStatement =>
            Nullable 
            ? $"""GetNodeOrNull<{Type}>("{NodePath}")""" 
            : $"""GetNode<{Type}>("{NodePath}")""";
    protected string NullableModifier => Nullable ? "?" : "";
    
    internal static OnReadyMember From(ISymbol symbol)
    {
        var attributeData = symbol.GetAttribute(OnReadyGenerator.OnReadyAttribute);

        return symbol switch
        {
            IPropertySymbol prop when prop.Type.IsNodeType() => From(prop, attributeData),
            IMethodSymbol method when method.ReturnType.IsNodeType() => From(method, attributeData),
            _ => throw new NotImplementedException($"Method of type {symbol.GetType()} is not supported.")
        };
    }

    private static OnReadyMember From(IPropertySymbol prop, AttributeData attributeData) =>
        new OnReadyProperty(
            prop.Name, 
            prop.DeclaredAccessibility, 
            prop.Type.FullNameGlobal(), 
            prop.Type.IsNullable(), 
            attributeData.GetFirstStringValueOrEmpty()
        );

    private static OnReadyMember From(IMethodSymbol method, AttributeData attributeData) =>
        new OnReadyMethod(
            method.Name, 
            method.DeclaredAccessibility, 
            method.ReturnType.FullNameGlobal(), 
            method.ReturnType.IsNullable(), 
            attributeData.GetFirstStringValueOrEmpty()
        );

    internal static bool Validate(INamedTypeSymbol classType, ISymbol symbol, SourceProductionContext ctx)
    {
        if (!symbol.HasAttribute(OnReadyGenerator.OnReadyAttribute)) return false;

        var validationInfo = OnReadyMemberValidationInfo.Create(symbol);
        var result = true;
        
        if (!classType.IsNodeType())
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    Analyzers.ParentClassMustDeriveFromNode,
                    symbol.Locations.First()
                )
            );
            result = false;
        }
        
        if (!validationInfo.Type.IsNodeType())
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    Analyzers.OnReadyMemberReturnMustDeriveFromNode,
                    symbol.Locations.First(),
                    symbol.Name
                )
            );
            result = false;
        }

        if (!validationInfo.IsPartial)
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    Analyzers.OnReadyMemberMustBeEmptyPartial,
                    symbol.Locations.First(),
                    symbol.Name
                )
            );
            result = false;
        }
        
        if (!validationInfo.IsReadOnly)
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    Analyzers.OnReadyPropertyIsNotReadOnly,
                    symbol.Locations.First(),
                    symbol.Name
                )
            );
            result = false;
        }

        if (validationInfo.IsStatic)
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    Analyzers.OnReadyMemberCannotBeStatic,
                    symbol.Locations.First(),
                    symbol.Name
                )
            );
            result = false;
        }

        if (!classType.IsPartial())
        {
            ctx.ReportDiagnostic(
                Diagnostic.Create(
                    Analyzers.ParentClassMustBePartial,
                    symbol.Locations.First()
                )
            );
            result = false;
        }

        return result;
    }
}
