# 🧩 SceneEntityAspect

The `SceneEntityAspect` class represents a reusable MonoBehaviour that can apply or discard behaviors on entities within a Unity scene. It comes in two forms:

* **Non-generic** version (`SceneEntityAspect`) for working with `IEntity`
* **Generic** version (`SceneEntityAspect<E>`) for specific entity types

---

## Key Features

### Aspect Application
- Encapsulates reusable logic or behavior for entities in the scene
- Can be applied or discarded dynamically
- Supports generic and non-generic usage for flexibility

### Type Safety
- Generic class allows compile-time type checking
- Non-generic class provides convenience when type specificity is not required

### Reusability
- Implemented as `MonoBehaviour`
- Can be attached to GameObjects in the scene and reused across multiple entities

---

## SceneEntityAspect
**A non-generic MonoBehaviour for scene entity aspects.**
```csharp
public abstract class SceneEntityAspect : SceneEntityAspect<IEntity>, IEntityAspect
{
    public abstract void Apply(IEntity entity);
    public abstract void Discard(IEntity entity);
}
```

## SceneEntityAspect<E>
**A generic MonoBehaviour for applying or discarding behaviors on a specific entity type.**
```csharp
public abstract class SceneEntityAspect<E> : MonoBehaviour, IEntityAspect<E> where E : IEntity
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

### SpeedBoost

The `SpeedBoost` aspect increases the speed of entities in the scene while attached. It multiplies the entity's `Speed` value when applied and restores it when discarded.

**Key Points:**
- Works with non-generic `SceneEntityAspect`
- Can be attached to a GameObject in the scene to automatically apply to tracked entities

```csharp
public sealed class SpeedBoost : SceneEntityAspect
{
    [SerializeField] private float _multiplier = 1.5f;

    public override void Apply(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value *= _multiplier;
    }

    public override void Discard(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value /= _multiplier;
    }
}
```

---

### JumpAspect

The `JumpAspect` adds jump capabilities to scene entities implementing `IGameEntity`. It sets tags, values, and behaviours required for jumping and removes them when discarded.

**Key Points:**
- Works with generic `SceneEntityAspect<IGameEntity>`
- Automatically updates the entity when applied or discarded

```csharp
public sealed class SceneJumpAspect : SceneEntityAspect<IGameEntity>
{
    [SerializeField] private float _jumpForce = 3f;

    public override void Apply(IGameEntity entity)
    {
        entity.AddTag("Jumpable");
        entity.AddValue("JumpForce", _jumpForce);
        entity.AddBehaviour<JumpBehaviour>();
    }

    public override void Discard(IGameEntity entity)
    {
        entity.DelTag("Jumpable");
        entity.DelValue("JumpForce");
        entity.DelBehaviour<JumpBehaviour>();
    }
}
```