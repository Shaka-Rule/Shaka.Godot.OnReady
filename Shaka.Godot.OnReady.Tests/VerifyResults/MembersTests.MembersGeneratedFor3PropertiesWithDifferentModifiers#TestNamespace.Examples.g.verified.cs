﻿//HintName: TestNamespace.Examples.g.cs
// <auto-generated/>
using Godot;
namespace TestNamespace;
#nullable enable
partial class Examples
{
    private global::Godot.Node2D? _player;
    public partial global::Godot.Node2D Player => _player ??= GetNode<global::Godot.Node2D>("Player");
    
    private global::Godot.Sprite2D? _sprite;
    protected partial global::Godot.Sprite2D Sprite => _sprite ??= GetNode<global::Godot.Sprite2D>("Player/Sprite");
    
    private global::Godot.Node2D? _sword;
    private partial global::Godot.Node2D Sword => _sword ??= GetNode<global::Godot.Node2D>("Player/Sword");
}
#nullable restore