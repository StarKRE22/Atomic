# 🧩 ScriptableEntityAspect&lt;E&gt;

```csharp
public abstract class ScriptableEntityAspect<E> : ScriptableObject, IEntityAspect<E> where E : IEntity
```

- **Description:** Represents a generic `ScriptableObject` that applies or discards reusable behavior on a strongly-typed entity.
- **Type Parameter:** `E` – The specific entity type this aspect operates on.
- **Inheritance:** `ScriptableObject`, [IEntityAspect&lt;E&gt;](IEntityAspect%601.md)
- **Note:** Great for type-specific buffs or debuffs applied to multiple entities.
- **See also:** [ScriptableEntityAspect](ScriptableEntityAspect.md)

---

## 🏹 Methods

#### `Apply(E)`

```csharp
public abstract void Apply(E entity);
```

- **Description:** Applies the aspect to the strongly-typed entity.
- **Parameter:** `entity` – The entity of type `E` to which the aspect will be applied.

#### `Discard(E)`

```csharp
public abstract void Discard(E entity);
```

- **Description:** Reverses the effects of `Apply` on the strongly-typed entity.
- **Parameter:** `entity` – The entity from which the aspect should be removed.

---

## 🗂 Example of Usage

Adds flying capabilities to a specific entity type implementing `IPlayerEntity`:

```csharp
public interface IPlayerEntity : IEntity
{
}
```

```csharp
[CreateAssetMenu(
    fileName = "PlayerFlyAspect",
    menuName = "SampleGame/New PlayerFlyAspect"
)]
public sealed class PlayerFlyAspect : ScriptableEntityAspect<IPlayerEntity>
{
    [SerializeField]
    private float _flyForce = 2f;

    public override void Apply(IPlayerEntity entity)
    {
        entity.AddTag("Flyable");
        entity.AddValue("FlyForce", _flyForce);
        entity.AddBehaviour<FlyBehaviour>();
    }

    public override void Discard(IPlayerEntity entity)
    {
        entity.DelTag("Flyable");
        entity.DelValue("FlyForce");
        entity.DelBehaviour<FlyBehaviour>();
    }
}
```

> Note: Using the generic `ScriptableEntityAspect<IPlayerEntity>` allows type-safe access to entity-specific properties without casting.