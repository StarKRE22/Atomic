# 🧩 IEntityAspect

The `IEntityAspect` interface represents a reusable piece of behavior or logic that can be applied to entities and discarded. It comes in two forms:

* **Non-generic** version (`IEntityAspect`) for working with `IEntity`
* **Generic** version (`IEntityAspect<E>`) for specific entity types

---

## Key Features

### Aspect Application
- Encapsulates reusable logic or behavior for entities
- Can be applied or discarded dynamically
- Supports generic and non-generic usage for flexibility

### Type Safety
- Generic interface allows compile-time type checking
- Non-generic interface provides convenience when type specificity is not required

### Reusability
- Can be implemented as `ScriptableObject` or `MonoBehaviour`
- Supports multiple entities using the same aspect instance

---

## IEntityAspect
**A shorthand interface for `IEntityAspect<IEntity>`.**
```csharp
public interface IEntityAspect : IEntityAspect<IEntity>
{
    void Apply(IEntity entity);
    void Discard(IEntity entity);
}
```

## IEntityAspect<E>
**A generic interface for applying or discarding behavior on a specific entity type.**
```csharp
public interface IEntityAspect<in E> where E : IEntity
{
    void Apply(E entity);
    void Discard(E entity);
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

### SpeedBuff

The `SpeedBuff` aspect temporarily increases the speed of any entity implementing `IEntity`. It multiplies the entity's `Speed` value by a configurable factor when applied, and restores it when discarded.

**Key Points:**
- Works with non-generic `IEntityAspect`.
- Uses `GetValue<IVariable<float>>("Speed")` to access the speed variable.
- Safe to apply and discard multiple times as long as the same multiplier is used consistently.


```csharp
public sealed class SpeedBuff : ScriptableObject, IEntityAspect
{
    [SerializeField] private float _multiplier = 2;
    
    public void Apply(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value *= _multiplier;
    }

    public void Discard(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value /= _multiplier;
    }
}
```

### JumpAspect

The `JumpAspect` aspect adds jump-related capabilities to entities implementing `IGameEntity`. It applies tags, values, and behaviours necessary for jumping and removes them when discarded.

**Key Points:**
- Works with generic `IEntityAspect<IGameEntity>`.
- Adds a "Jumpable" tag to mark the entity as able to jump.
- Adds a `JumpForce` value and attaches a `JumpBehaviour`.
- Discarding removes the tag, value, and behaviour, fully cleaning up the entity.

```csharp
public sealed class JumpAspect : ScriptableObject, IEntityAspect<IGameEntity>
{
    [SerializeField] private float _jumpForce = 2;
    
    public void Apply(IGameEntity entity)
    {
        entity.AddTag("Jumpable");
        entity.AddValue("JumpForce", _jumpForce);
        entity.AddBehaviour<JumpBehaviour>();
    }

    public void Discard(IGameEntity entity)
    {
        entity.DelTag("Jumpable");
        entity.DelValue("JumpForce");
        entity.DelBehaviour<JumpBehaviour>();
    }
}
```
