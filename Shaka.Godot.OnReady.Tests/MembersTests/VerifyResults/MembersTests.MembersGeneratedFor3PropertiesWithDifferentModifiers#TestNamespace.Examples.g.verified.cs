﻿//HintName: TestNamespace.Examples.g.cs
// <auto-generated/>
using Godot;
namespace TestNamespace;
#nullable enable
partial class Examples
{
    private global::Godot.Node2D? _player
    {
        [Obsolete("This is the backing field for the autogenerated OnReady member 'Player'. Please use 'Player' to access the value.")]
        get; 
        set;
    }
    public partial global::Godot.Node2D Player => _player ??= GetNode<global::Godot.Node2D>("Player");
    
    private global::Godot.Sprite2D? _sprite
    {
        [Obsolete("This is the backing field for the autogenerated OnReady member 'Sprite'. Please use 'Sprite' to access the value.")]
        get; 
        set;
    }
    protected partial global::Godot.Sprite2D Sprite => _sprite ??= GetNode<global::Godot.Sprite2D>("Player/Sprite");
    
    private global::Godot.Node2D? _sword
    {
        [Obsolete("This is the backing field for the autogenerated OnReady member 'Sword'. Please use 'Sword' to access the value.")]
        get; 
        set;
    }
    private partial global::Godot.Node2D Sword => _sword ??= GetNode<global::Godot.Node2D>("Player/Sword");
}
#nullable restore