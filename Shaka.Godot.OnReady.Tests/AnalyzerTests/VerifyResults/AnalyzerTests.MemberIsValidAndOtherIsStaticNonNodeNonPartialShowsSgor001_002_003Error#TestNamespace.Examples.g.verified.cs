﻿//HintName: TestNamespace.Examples.g.cs
// <auto-generated/>
using Godot;
namespace TestNamespace;
#nullable enable
partial class Examples
{
    private global::Godot.Node2D? _onReadyNode2DValid
    {
        [Obsolete("This is the backing field for the autogenerated OnReady member 'OnReadyNode2DValid'. Please use 'OnReadyNode2DValid' to access the value.")]
        get; 
        set;
    }
    public partial global::Godot.Node2D OnReadyNode2DValid => _onReadyNode2DValid ??= GetNode<global::Godot.Node2D>("frezfrezf");
    
    private global::Godot.Node3D? _onReadyNode3DValid
    {
        [Obsolete("This is the backing field for the autogenerated OnReady member 'OnReadyNode3DValid'. Please use 'OnReadyNode3DValid' to access the value.")]
        get; 
        set;
    }
    public partial global::Godot.Node3D OnReadyNode3DValid()
    {
        return _onReadyNode3DValid ??= GetNode<global::Godot.Node3D>("/root/mynode");
    }
}
#nullable restore