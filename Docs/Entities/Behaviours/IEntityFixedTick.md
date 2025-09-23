# 🧩️ IEntityFixedUpdate

`IEntityFixedUpdate` is a behavior interface that executes logic during the **fixed update cycle** of an `IEntity`.  
It is automatically invoked by the entity’s `OnFixedUpdate` method at a consistent interval, typically used for physics or time-sensitive calculations.

---

## Key Features

- **Fixed-Timestep Logic** – Executes routines at consistent intervals, independent of frame rate.
- **Strongly-Typed Option** – `IEntityFixedUpdate<E>` allows type-specific fixed update logic.
- **Automatic Invocation** – Called automatically by `IEntity.OnFixedUpdate`.
- **Composable** – Can be combined with other behaviours for modular entity logic.

---

## Interface: IEntityFixedUpdate
```csharp
public interface IEntityFixedUpdate : IEntityBehaviour
{
    void FixedUpdate(IEntity entity, float fixedDeltaTime);
}
```
---

## Interface: IEntityFixedUpdate&lt;E&gt;
```csharp
public interface IEntityFixedUpdate<in E> : IEntityFixedUpdate where E : IEntity
{
    void OnFixedUpdate(E entity, float fixedDeltaTime);
}
```

- Implements `IEntityFixedUpdate.FixedUpdate(IEntity)` automatically by casting to `E`.
- Ensures type-safe fixed update logic for specific entity types.

---

## Example Usage
Move a `Rigidbody` component at a fixed interval for an entity

### Example #1. Non-Generic (IEntity)
```csharp
public class MoveUnitFixedBehaviour : IEntityFixedUpdate
{
    public void OnFixedUpdate(IEntity entity, float fixedDeltaTime)
    {
        var rigidbody = entity.GetValue<Rigidbody>("Rigidbody");
        var speed = entity.GetValue<float>("Speed");
        var direction = entity.GetValue<Vector3>("Direction");
        rigidbody.MovePosition(rigidbody.position + direction * (speed * fixedDeltaTime));
    }
}
```

> Note: `GetValue<T>` assumes the entity has the relevant components and values already set.

### Example #2. Generic with UnitEntity (strongly-typed)
```csharp
public class MoveUnitFixedBehaviour : IEntityFixedUpdate<UnitEntity>
{
    public void OnFixedUpdate(UnitEntity entity, float fixedDeltaTime)
    {
        var rigidbody = entity.GetValue<Rigidbody>("Rigidbody");
        var speed = entity.GetValue<float>("Speed");
        var direction = entity.GetValue<Vector3>("Direction");
        rigidbody.MovePosition(rigidbody.position + direction * (speed * fixedDeltaTime));
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.

## Remarks

- `IEntityFixedUpdate` is intended for time-sensitive logic that must run at a fixed timestep.
- `IEntityFixedUpdate<E>` is useful when the behaviour is specific to a particular entity type.
- Behaviours can interact with other entity behaviours during the fixed update cycle.
- Does not handle initialization, enabling, disabling, updating, or disposal; separate interfaces exist for those phases (`IEntityInit`, `IEntityEnable`, `IEntityDisable`, `IEntityUpdate`, `IEntityDispose`).  
