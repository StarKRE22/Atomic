# 🧩 ScriptableEntityAspect

The `ScriptableEntityAspect` class represents a reusable ScriptableObject that can apply or discard behaviors on entities. It comes in two forms:

* **Non-generic** version (`ScriptableEntityAspect`) for working with `IEntity`
* **Generic** version (`ScriptableEntityAspect<E>`) for specific entity types

---

## Key Features

### Aspect Application
- Encapsulates reusable logic or behavior for entities
- Can be applied or discarded dynamically
- Supports generic and non-generic usage for flexibility

### Type Safety
- Generic class allows compile-time type checking
- Non-generic class provides convenience when type specificity is not required

### Reusability
- Implemented as `ScriptableObject`
- Can be stored as an asset and reused across multiple entities

---

## ScriptableEntityAspect
**A non-generic base class for scriptable entity aspects.**
```csharp
public abstract class ScriptableEntityAspect : ScriptableEntityAspect<IEntity>, IEntityAspect
{
    public abstract void Apply(IEntity entity);
    public abstract void Discard(IEntity entity);
}
```

## ScriptableEntityAspect<E>
**A generic base class for applying or discarding behaviors on a specific entity type.**
```csharp
public abstract class ScriptableEntityAspect<E> : ScriptableObject, IEntityAspect<E> where E : IEntity
{
    public abstract void Apply(E entity);
    public abstract void Discard(E entity);
}
```

---

## Methods

### Apply
```csharp
void Apply(E entity);
```
- **Purpose**: Applies the aspect to the specified entity
- **Parameter**: `entity` — The entity to which the aspect will be applied
- **Behavior**: Implements logic, changes, or effects on the entity

### Discard
```csharp
void Discard(E entity);
```
- **Purpose**: Reverses the effects of `Apply` on the specified entity
- **Parameter**: `entity` — The entity from which the aspect should be removed
- **Behavior**: Cleans up or removes applied logic from the entity

---

## Example Usage

### DamageBoost

The `DamageBoost` aspect increases the damage of an entity implementing `IEntity`. It can be applied and discarded dynamically.

**Key Points:**
- Works with non-generic `ScriptableEntityAspect`
- Can be stored as a ScriptableObject asset for reuse

```csharp
public sealed class DamageBoost : ScriptableEntityAspect
{
    [SerializeField] private float _bonusDamage = 50f;

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

---

### PlayerFlyAspect

The `PlayerFlyAspect` adds flying capability to a specific entity type implementing `IGameEntity`. It applies and removes tags, values, and behaviours as needed.

**Key Points:**
- Works with generic `ScriptableEntityAspect<IGameEntity>`
- Can encapsulate reusable logic for multiple entities

```csharp
public sealed class PlayerFlyAspect : ScriptableEntityAspect<IPlayerEntity>
{
    [SerializeField] private float _flyForce = 2f;

    public override void Apply(IPlayerEntity entity)
    {
        entity.AddTag("Flyable");
        entity.AddValue("FlyForce", _jumpForce);
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