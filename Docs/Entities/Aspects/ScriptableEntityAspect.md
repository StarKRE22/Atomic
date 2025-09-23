# 🧩 ScriptableEntityAspect Classes

Represents a reusable `ScriptableObject` that can **apply or discard behaviors on [entities](../Entities/IEntity.md)**.
Use `ScriptableEntityAspect` for lightweight, reusable logic such as buffs and debuffs that can be dynamically applied
or removed from entities.

---

<details>
  <summary>
    <h2 id="scriptable-entity-aspect"> 🧩 ScriptableEntityAspect</h2>
    <br>Non-generic base class for applying/discarding behavior on any <code>IEntity</code>.
  </summary>

<br>

```csharp
public abstract class ScriptableEntityAspect : ScriptableEntityAspect<IEntity>, IEntityAspect
```

- **Inheritance:** Extends [ScriptableEntityAspect&lt;IEntity&gt;](#scriptable-entity-aspect-e) and
  implements [IEntityAspect](IEntityAspect.md)
- **Description:** Applies or discards reusable behavior for any entity implementing `IEntity`.
- **Use Case:** Ideal for lightweight buffs or debuffs stored as ScriptableObject assets.

---

### 🏹 Methods

#### `Apply(IEntity)`

```csharp
public abstract void Apply(IEntity entity);
```

- **Description:** Applies the aspect to the specified entity.
- **Parameters:** `entity` – The entity to which the aspect will be applied.

#### `Discard(IEntity)`

```csharp
public abstract void Discard(IEntity entity);
```

- **Description:** Reverses the effects of `Apply` on the specified entity.
- **Parameters:** `entity` – The entity from which the aspect should be removed.

---

### 🗂 Example of Usage

The `DamageBoost` aspect temporarily increases an entity's damage.

```csharp
[CreateAssetMenu(
    fileName = "DamageBoost",
    menuName = "SampleGame/New DamageBoost"
)]
public sealed class DamageBoost : ScriptableEntityAspect
{
    [SerializeField]
    private float _bonusDamage = 50f;

    public override void Apply(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Damage").Value += _bonusDamage;
    }

    public override void Discard(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Damage").Value -= _bonusDamage;
    }

}
```

</details>

---

<details>
  <summary>
    <h2 id="scriptable-entity-aspect-e"> 🧩 ScriptableEntityAspect&lt;E&gt;</h2>
    <br>Generic base class for applying/discarding behavior on a specific entity type.
  </summary>

<br>

```csharp
public abstract class ScriptableEntityAspect<E> : ScriptableObject, IEntityAspect<E> where E : IEntity
```

- **Type Parameter:** `E` – The specific entity type this aspect operates on.
- **Description:** Provides type-safe application and discard of behavior for a specific entity type.
- **Inheritance:** Implements [IEntityAspect&lt;E&gt;](IEntityAspect.md/#ienity-aspect-e)
- **Use Case:** Great for type-specific buffs/debuffs applied to multiple entities.

---

### 🏹 Methods

#### `Apply(E)`

```csharp
public abstract void Apply(E entity);
```

- **Description:** Applies the aspect to the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to which the aspect will be applied.

#### `Discard(E)`

```csharp
public abstract void Discard(E entity);
```

- **Description:** Reverses the effects of `Apply` on the strongly-typed entity.
- **Parameters:** `entity` – The entity from which the aspect should be removed.

---

### 🗂 Example of Usage

The `PlayerFlyAspect` adds flying capabilities to a specific entity type implementing `IPlayerEntity`.

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

> Note: Using the generic `ScriptableEntityAspect<IPlayerEntity>` allows type-safe access to entity-specific properties
> without casting.

</details>

---

## 📝 Notes

- **Dynamic Logic** – Aspects encapsulate modular behavior that can be applied and discarded dynamically.
- **Generic Option** – `ScriptableEntityAspect<E>` allows compile-time type safety for specific entity types.
- **Non-Generic Option** – `ScriptableEntityAspect` works with any `IEntity` for convenience.
- **Implementation** – Implemented as `ScriptableObject`, can be stored as assets and reused across multiple entities.
- **Reusability** – Ideal for buffs and debuffs; lightweight pattern allows multiple entities to share the same aspect.