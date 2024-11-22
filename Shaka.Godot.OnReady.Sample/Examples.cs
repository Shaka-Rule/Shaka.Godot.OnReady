using Godot;

namespace Shaka.Godot.OnReady.Sample;

public partial class Examples : Node3D
{
    [OnReady("Player")]
    public partial Node2D Player { get; }

    [OnReady("Player2")]
    public partial Node2D Player2();
}