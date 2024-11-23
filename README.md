# Shaka.Godot.OnReady

Source generator for Godot 4 .NET version.
It adds an OnReady attribute which automatically implements a property or method which calls GetNode or GetNodeOrNull.

## Setup

Add the Shaka.Godot.OnReady package through nuget package manager.

Or add this to your csproj file:
```xml
<ItemGroup>
    <PackageReference Include="Shaka.Godot.OnReady" Version="1.0.0"/>
</ItemGroup>
```

## Usage

Add the Onready attribute to any partial property or method in a class deriving from Godot.Node.

### Property(Only available from .NET9):
```csharp
public partial class Player : CharacterBody2D
{
    [OnReady("%Sprite2D")]
    private partial Sprite2D Sprite { get; }
}
```
This will generate:
```csharp
public partial class Player
{
    private Sprite2D? _sprite;
    private Sprite2D Sprite => _sprite ??= GetNode("%Sprite2D");
}
```

### Method:
```csharp
public partial class Player : CharacterBody2D
{
    [OnReady("%Sprite2D")]
    private partial Sprite2D Sprite();
}
```
This will generate:
```csharp
public partial class Player
{
    private Sprite2D? _sprite;
    private Sprite2D Sprite
    {
        return _sprite ??= GetNode("%Sprite2D");
    }
}
```

### Nullable
When using a nullable type, the generator will instead generate this:
```csharp
public partial class Player
{
    private Sprite2D? _sprite;
    private Sprite2D? Sprite => _sprite ??= GetNodeOrNull("%Sprite2D");
}
```

This way your code will not crash when getnode fails and you can handle nulls yourself.

