using System.Collections.Generic;
using System.Linq;

namespace Shaka.Godot.OnReady;

internal record OnReadyClass(string FullName, List<OnReadyMember> Members) : ISourceText
{
    public string ToSource()
    {
        var namespaceStatement = Namespace.Length > 0 ? $"namespace {Namespace};" : string.Empty;

        var onReadyClass = $$"""
                             // <auto-generated/>
                             using Godot;
                             {{namespaceStatement}}
                             #nullable enable
                             partial class {{Name}}
                             {
                                 {{string.Join("\n\n", Members.Select(m => m.ToSource())).Indent(1)}}
                             }
                             #nullable restore
                             """;
        return onReadyClass;
    }

    public string Name
    {
        get
        {
            if (string.IsNullOrEmpty(field))
            {
                field = FullName.Split('.').Last();
            }

            return field;
        }
    } = string.Empty;

    public string Namespace
    {
        get
        {
            if (string.IsNullOrEmpty(field))
            {
                field = NamespaceFromFullName();
            }

            return field;
        }
    } = string.Empty;

    public string FullNameWithoutGlobal
    {
        get
        {
            if (string.IsNullOrEmpty(field))
            {
                field = FullName.Split(':', ':')[1];
            }

            return field;
        }
    } = string.Empty;

    private string NamespaceFromFullName()
    {
        var parts = FullName.Split('.');
        return string.Join(".", parts.Take(parts.Length - 1));
    }
}
