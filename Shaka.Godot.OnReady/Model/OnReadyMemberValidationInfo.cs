using System;
using Microsoft.CodeAnalysis;

namespace Shaka.Godot.OnReady;

internal record OnReadyMemberValidationInfo(ITypeSymbol Type, bool IsPartial, bool IsStatic, bool IsReadOnly)
{
    internal static OnReadyMemberValidationInfo Create(ISymbol symbol)
    {
        
        ITypeSymbol type;
        bool isPartial;
        var isStatic = symbol.IsStatic;
        var isReadOnly = true;
        switch (symbol)
        {
            case IPropertySymbol prop:
                type = prop.Type;
                isPartial = prop.IsPartialDefinition;
                isReadOnly = prop.IsReadOnly;
                break;
            case IMethodSymbol method:
                type = method.ReturnType;
                isPartial = method.IsPartialDefinition;
                break;
            default:
                throw new ArgumentException($"Method of type {symbol.GetType()} is not supported.");
        }

        return new OnReadyMemberValidationInfo(type, isPartial, isStatic, isReadOnly);
    }
}