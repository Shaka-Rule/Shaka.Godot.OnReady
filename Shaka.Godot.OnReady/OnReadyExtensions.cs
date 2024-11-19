using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Shaka.Godot.OnReady;

public static partial class OnReadyExtensions
{
    public static string Indent(this string s, int amount)
    {
        var indent = new string(' ', 4 * amount);
        var lines = s.Split('\n');
        
        return string.Join($"\n{indent}", lines);
    }

    public static bool HasAttribute(this MemberDeclarationSyntax member, string attribute)
    {
        return member.AttributeLists.Any(al => al.Attributes.Any(a => a.Name.ToString() == attribute));
    }

    public static bool HasAttribute(this ISymbol symbol, string attribute)
    {
        return symbol.GetAttributes().Any(s => s?.AttributeClass?.Name == attribute);
    }

    public static bool TryGetAttribute(this ISymbol symbol, string attribute, out AttributeData? attributeData)
    {
        attributeData = symbol.GetAttributes().FirstOrDefault(s => s?.AttributeClass?.Name == attribute);
        return attributeData is not null;
    }

    public static AttributeData GetAttribute(this ISymbol symbol, string attribute)
    {
        return symbol.GetAttributes().First(s => s?.AttributeClass?.Name == attribute);
    }
    
    public static bool AnyMemberHasAttribute(this ClassDeclarationSyntax classDeclarationSyntax, string attribute, params SyntaxKind[] kinds)
    {
        return classDeclarationSyntax.Members.Any(member => kinds.Any(member.IsKind) && member.HasAttribute(attribute));
    }

    public static string GetFirstStringValueOrEmpty(this AttributeData attributeData)
    {
        return attributeData.ConstructorArguments[0].Value?.ToString() ?? string.Empty;
    }
    private static readonly SymbolDisplayFormat FullNameFormat = SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Omitted);
    private static readonly SymbolDisplayFormat FullNameGlobalFormat  = SymbolDisplayFormat.FullyQualifiedFormat.WithGlobalNamespaceStyle(SymbolDisplayGlobalNamespaceStyle.Included);
    
    public static string FullName(this ITypeSymbol symbol) => symbol.ToDisplayString(FullNameFormat);
    public static string FullNameGlobal(this ITypeSymbol symbol) => symbol.ToDisplayString(FullNameGlobalFormat);
    
    public static bool InheritsFrom(this ITypeSymbol? symbol, string typeFullName)
    {
        while (symbol != null)
        {
            if (symbol.ContainingAssembly?.Name == "GodotSharp" &&
                symbol.FullName() == typeFullName)
            {
                return true;
            }
            symbol = symbol.BaseType;
        }
        return false;
    }

    public static bool IsNodeType(this ITypeSymbol? symbol) => symbol.InheritsFrom("Godot.Node");
}