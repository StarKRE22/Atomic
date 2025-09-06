# 🧩 IEntityUpdate

`IEntityUpdate` is a behavior interface that executes logic during the **regular update cycle** of an `IEntity`.  
It is automatically invoked by the entity’s `OnUpdate` method once per frame during the main game loop.

---

## Key Features

- **Per-Frame Logic** – Executes routines each frame during the game loop.
- **Strongly-Typed Option** – `IEntityUpdate<E>` allows type-specific update logic.
- **Automatic Invocation** – Called automatically by `IEntity.OnUpdate`.
- **Composable** – Can be combined with other behaviours for modular entity logic.

---

## Interface: IEntityUpdate

```csharp
public interface IEntityUpdate : IEntityBehaviour
{
    void Update(IEntity entity, float deltaTime);
}
```

---

## Interface: IEntityUpdate&lt;E&gt;
```csharp
public interface IEntityUpdate<in E> : IEntityUpdate where E : IEntity
{
    void OnUpdate(E entity, float deltaTime);
}
```

- Implements `IEntityUpdate.Update(IEntity)` automatically by casting to `E`.
- Ensures type-safe update logic for specific entity types.

---

## Example Usage
Update a `Transform` component every frame for an entity

### Example #1. Non-Generic (IEntity)
```csharp
public class MoveUnitBehaviour : IEntityUpdate
{
    public void OnUpdate(IEntity entity, float deltaTime)
    {
        var transform = entity.GetValue<Transform>("Transform");
        var speed = entity.GetValue<float>("Speed");
        var direction = entity.GetValue<Vector3>("Direction");
        transform.position += direction * (speed * deltaTime);
    }
}
```

> Note: `GetValue<T>` assumes the entity has the relevant components and values already set.

### Example #2. Generic with UnitEntity (strongly-typed)
```csharp
public class MoveUnitBehaviour : IEntityUpdate<UnitEntity>
{
    public void OnUpdate(UnitEntity entity, float deltaTime)
    {
        var transform = entity.GetValue<Transform>("Transform");
        var speed = entity.GetValue<float>("Speed");
        var direction = entity.GetValue<Vector3>("Direction");
        transform.position += direction * (speed * deltaTime);
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.

## Remarks

- `IEntityUpdate` is intended for per-frame logic that must run during the game loop.
- `IEntityUpdate<E>` is useful when the behaviour is specific to a particular entity type.
- Behaviours can interact with other entity behaviours during updates.
- Does not handle initialization, enabling, disabling, or disposal; separate interfaces exist for those phases (`IEntityInit`, `IEntityEnable`, `IEntityDisable`, `IEntityDispose`).  
