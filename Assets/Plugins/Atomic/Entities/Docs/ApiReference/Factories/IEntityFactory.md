# 🧩️ IEntityFactory

`IEntityFactory` is a factory interface responsible for creating new instances of `IEntity`.  
It provides both a **non-generic** and a **generic** version, making it useful in scenarios where entities are created dynamically at runtime (e.g., registries, catalogs, or data-driven systems).

---

## Key Features

- **Factory Pattern** – Encapsulates the creation of entity instances.
- **Generic & Non-Generic** – Supports both `IEntity` and strongly-typed factories.
- **Configurable Instantiation** – Implementations may apply default tags, values, or behaviours during creation.
- **Integration Friendly** – Commonly used with registries, pools, and data-driven systems such as `ScriptableObject` or `MonoBehaviour`.
- **Builder Support** – Factories can be extended into builders, allowing pre-configuration before entity creation.

---

## Interface: IEntityFactory (Non-generic)

```csharp
public interface IEntityFactory : IEntityFactory<IEntity>
{
}
```

- Non-generic factory for heterogeneous contexts.
- Useful when working with multiple entity types in a single registry or catalog.

---

## Interface: IEntityFactory&lt;E&gt; (Generic)
```csharp
public interface IEntityFactory<out E> where T : IEntity
{
    T Create();
}
```
- Strongly-typed factory that produces instances of a specific entity type.
- Ensures type safety and avoids unnecessary casting.
- Can configure entity instances before returning them.

---

## Example Usage

### Example #1. Non-Generic Factory
```csharp
public class BasicEntityFactory : IEntityFactory
{
    public IEntity Create()
    {
        var entity = new Entity();
        entity.SetValue("Health", 100);
        entity.SetValue("Name", "Unnamed");
        return entity;
    }
}
```

> Useful in registries that handle multiple different entity types without knowing them at compile time.

---

### Example #2. Generic Factory (Typed)
```csharp
public class UnitEntityFactory : IEntityFactory<UnitEntity>
{
    public UnitEntity Create()
    {
        var unit = new UnitEntity();
        unit.SetValue("Health", 150);
        unit.SetValue("AttackPower", 25);
        return unit;
    }
}
```

> This approach is type-safe and avoids casting, ideal for systems working with specific entity types.

---

### Example #3. Using Factory as a Builder
Factories can also act like **builders**, allowing configuration before creation.  
This is useful when the entity requires external dependencies or custom initialization.

```csharp
public sealed class PlayerContextBuilder : IEntityFactory<PlayerContext>
{
    private GameContext _gameContext;
    private TeamType _teamType;
    private Camera _camera;

    public PlayerContextBuilder SetGameContext(GameContext gameContext)
    {
        _gameContext = gameContext; return this;
    }

    public PlayerContextBuilder SetTeamType(TeamType teamType)
    {
        _teamType = teamType; return this;
    }
    
   public PlayerContextBuilder SetCamera(Camera camera)
    {
        _camera = camera; return this;
    }

    public override PlayerContext Create()
    {
        if (_camera == null)  
            throw new InvalidOperationException("Camera must be set before creating PlayerContext.");  

        if (_teamType == default)  
            throw new InvalidOperationException("TeamType must be set before creating PlayerContext.");  

        var playerContext = new PlayerContext();
        playerContext.AddValue<TeamType>("TeamType", _teamType);
        playerContext.AddValue<GameContext>("GameContext", _gameContext);
        playerContext.AddValue<Camera>("Camera", _camera);
        return playerContext;
    }
}
```

```csharp
//You can create a lot of players with different settings
var playerContext = new PlayerContextBuilder()
    .SetGameContext(gameContext)
    .SetTeamType(teamType)
    .SetCamera(camera)
    .Create();
```

> In this pattern, the factory provides `Set...` methods to configure parameters,  
> and the final `Create()` produces a fully prepared `IPlayerContext`.


## Remarks

- `IEntityFactory` is most useful for registries, catalogs, and data-driven systems where entities are created dynamically.
- The generic form `IEntityFactory<T>` should be preferred when working with a single known entity type.
- Factories may be combined with pooling systems for efficient runtime entity management.  
