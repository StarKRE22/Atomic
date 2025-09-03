# 🧩️ ScriptableEntityFactory

`ScriptableEntityFactory` is an **abstract Unity-based factory** for creating and configuring `IEntity` instances.  
It leverages Unity’s `ScriptableObject` to store initial parameters (**tags**, **values**, **behaviours**) and provides hooks for extending entity creation logic.

---

## Key Features

- **Unity Integration** – Built on top of `ScriptableObject`, easily configurable in the Unity Inspector.
- **Predefined Parameters** – Stores initial tag, value, and behaviour counts for optimization.
- **Custom Installation** – Extend `Install(IEntity)` to inject components, behaviours, or extra configuration.
- **Editor Support** – Automatically updates cached metadata via `OnValidate` and `Precompile`.
- **Generic Version** – `ScriptableEntityFactory<E>` allows strongly-typed entity creation.
- **Lightweight & Reusable** – Can be reused across multiple objects without heavy dependencies.
> Note: `[SerializeField]` fields (`InitialTagCount`, `InitialValueCount`, `InitialBehaviourCount`) are primarily used for **Editor optimization** and can be adjusted in the Inspector to preallocate resources for entities.

---

## Class: ScriptableEntityFactory (Non-Generic)

```csharp
public abstract class ScriptableEntityFactory : ScriptableEntityFactory<IEntity>, IEntityFactory
{
    public sealed override IEntity Create()
    {
        var entity = new Entity(
            this.name,
            this.InitialTagCount,
            this.InitialValueCount,
            this.InitialBehaviourCount
        );
        this.Install(entity);
        return entity;
    }

    protected abstract void Install(IEntity entity);
}
```
- Provides a **sealed** `Create()` method that constructs a base `Entity`.
- Subclasses implement `Install(IEntity)` to configure entities with custom behaviours.

---

## Class: ScriptableEntityFactory&lt;E&gt; (Generic)
```csharp
public abstract class ScriptableEntityFactory<E> : ScriptableObject, IEntityFactory<E> where E : IEntity
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
            Debug.LogWarning($"{nameof(ScriptableEntityFactory<E>)}: Create() returned null.", this);
        }
    }
}
```
---

## Example Usage

### Example #1. Simple Scriptable Factory
```csharp
[CreateAssetMenu(menuName = "Factories/Enemy")]
public class EnemyFactory : ScriptableEntityFactory<EnemyEntity>
{
    public override EnemyEntity Create()
    {
        var enemy = new EnemyEntity();
        enemy.AddValue<int>("Health", 100);
        enemy.AddValue<int>("Damage", 10);
        return enemy;
    }
}
```
---

### Example #2. Custom Installation
```csharp
[CreateAssetMenu(menuName = "Factories/Character")]
public class CharacterFactory : ScriptableEntityFactory
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
This builder is **lightweight** — stores only simple state and can be reused by multiple systems without performance cost.

```csharp
public sealed class PlayerContextBuilder : ScriptableEntityFactory<PlayerContext>
{
    private GameContext _gameContext;
    private TeamType _teamType;
    private Camera _camera;

    public PlayerContextBuilder SetGameContext(GameContext gameContext)
    {
        _gameContext = gameContext;
        return this;
    }

    public PlayerContextBuilder SetTeamType(TeamType teamType)
    {
        _teamType = teamType;
        return this;
    }

    public PlayerContextBuilder SetCamera(Camera camera)
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
    [SerializeField] 
    private PlayerContextBuilder _playerBuilder;

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

## Remarks
- Use **non-generic** `ScriptableEntityFactory` for heterogeneous entities in registries or catalogs.
- Use **generic** `ScriptableEntityFactory<E>` for type-safe creation when the entity type is known.
- `Precompile` caches metadata (`TagCount`, `ValueCount`, `BehaviourCount`) to reduce runtime introspection.
- `Install(IEntity)` allows injecting behaviours or components into newly created entities.
- Builder-style factories are **lightweight** and suitable for reusing across multiple objects without performance overhead.  
