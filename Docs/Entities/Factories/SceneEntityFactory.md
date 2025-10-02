# 🧩️ SceneEntityFactory

`SceneEntityFactory` is an **abstract Unity-based factory** for creating and configuring `IEntity` instances in **scene-based workflows**.  
It is designed for **runtime instantiation, prototyping, and scene baking**, allowing custom logic to be applied after creation via `Install`.

---

## Key Features

- **Predefined Parameters** – Stores initial tag, value, and behaviour counts for optimization.


---

## Class: SceneEntityFactory (Non-Generic)


- Provides a **sealed** `Create()` method that constructs a base `Entity`.
- Subclasses implement `Install(IEntity)` to apply custom configurations.

---

## Class: SceneEntityFactory&lt;E&gt; (Generic)
```csharp
public abstract class SceneEntityFactory<E> : MonoBehaviour, IEntityFactory<E> where E : IEntity
{
    [SerializeField] protected int InitialTagCount;
    [SerializeField] protected int InitialValueCount;
    [SerializeField] protected int InitialBehaviourCount;

    public abstract E Create();

    protected virtual void Reset()
    {
        this.InitialTagCount = 0;
        this.InitialValueCount = 0;
        this.InitialBehaviourCount = 0;
    }

    protected virtual void Precompile()
    {
        E entity = this.Create();
        if (entity != null)
        {
            this.InitialTagCount = entity.TagCount;
            this.InitialValueCount = entity.ValueCount;
            this.InitialBehaviourCount = entity.BehaviourCount;
        }
        else
        {
            Debug.LogWarning($"{nameof(SceneEntityFactory<E>)}: Create() returned null.", this);
        }
    }
}
```
---

## Example Usage

### Example #1. Simple Scene Factory
```csharp
public class EnemySceneFactory : SceneEntityFactory<EnemyEntity>
{
    public override EnemyEntity Create()
    {
        var enemy = new EnemyEntity();
        enemy.AddValue<int>("Health", 100);
        enemy.AddValue<int>("Damage", 15);
        return enemy;
    }
}
```

---

### Example #2. Custom Installation
```csharp
public class CharacterSceneFactory : SceneEntityFactory
{
    protected override void Install(IEntity entity)
    {
        entity.AddValue<int>("Health", 200);
        entity.AddValue<string>("Name", "Hero");
        entity.AddBehaviour<DeathBehaviour>();
    }
}
```
---

### Example #3. Using Factory as a Builder

This builder is **lightweight** — can be reused across multiple objects and configured via fluent interface.

```csharp
public sealed class PlayerContextSceneBuilder : SceneEntityFactory<PlayerContext>
{
    private GameContext _gameContext;
    private TeamType _teamType;
    private Camera _camera;

    public PlayerContextSceneBuilder SetGameContext(GameContext gameContext)
    {
        _gameContext = gameContext;
        return this;
    }

    public PlayerContextSceneBuilder SetTeamType(TeamType teamType)
    {
        _teamType = teamType;
        return this;
    }

    public PlayerContextSceneBuilder SetCamera(Camera camera)
    {
        _camera = camera;
        return this;
    }

    public override PlayerContext Create()
    {
        if (_gameContext == null)
            throw new InvalidOperationException("GameContext must be set before creating PlayerContext.");
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
// Usage:
public class Example : MonoBehaviour
{
    [SerializeField] private PlayerContextSceneBuilder _playerBuilder;

    private void Start()
    {
        var playerContext = _playerBuilder
            .SetGameContext(gameContext)
            .SetTeamType(teamType)
            .SetCamera(camera)
            .Create();
    }
}
```

---

## Remarks

- Use **non-generic** `SceneEntityFactory` for heterogeneous entities in scene workflows.
- Use **generic** `SceneEntityFactory<E>` for type-safe creation when the entity type is known.
- `Precompile` caches metadata (`TagCount`, `ValueCount`, `BehaviourCount`) to optimize editor previews and scene baking.
- `Install(IEntity)` allows injecting behaviours, components, or custom configuration after creation.
- Builder-style factories are **lightweight** and suitable for multiple reuses without performance overhead.  
