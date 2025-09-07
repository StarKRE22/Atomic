# 🧩️️ IEntityDispose

`IEntityDispose` is a behavior interface that executes cleanup or resource release logic when an `IEntity` **is being disposed**.  
It is automatically invoked by the entity’s `Dispose` method when the entity is permanently destroyed, removed from the game, or otherwise released from use.

---

## Key Features

- **Cleanup Logic** – Executes routines to release references, unsubscribe from events, or return pooled resources.
- **Strongly-Typed Option** – `IEntityDispose<E>` allows type-specific disposal logic.
- **Automatic Invocation** – Called automatically by `IEntity.Dispose`.
- **Composable** – Can be combined with other behaviours for modular entity logic.

---

## Interface: IEntityDispose

```csharp
public interface IEntityDispose : IEntityBehaviour
{
    void Dispose(IEntity entity);
}
```

---

## Interface: IEntityDispose&lt;E&gt;

```csharp
public interface IEntityDispose<in E> : IEntityDispose where E : IEntity
{
    void Dispose(E entity);
}
```

- Implements `IEntityDispose.Dispose(IEntity)` automatically by casting to `E`.
- Ensures type-safe disposal logic for specific entity types.

---

## Example Usage
Dispose a `Renderer` component when an entity is being disposed

### Example #1. Non-Generic (IEntity)
```csharp
public class DisposeRendererBehaviour : IEntityDispose
{
    public void Dispose(IEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        Object.Destroy(renderer);
    }
}
```

> Note: `GetValue<T>` assumes the entity has a `Renderer` component already set.

### Example #2. Generic with UnitEntity (strongly-typed)
```csharp
public class DisposeRendererBehaviour : IEntityDispose<UnitEntity>
{
    public void Dispose(UnitEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        Object.Destroy(renderer);
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.

## Remarks

- `IEntityDispose` is intended for logic that must run when an entity is permanently removed.
- `IEntityDispose<E>` is useful when the behaviour is specific to a particular entity type.
- Behaviours can interact with other entity behaviours during disposal.
- Does not handle initialization, enabling, or updating; separate interfaces exist for those phases (`IEntityInit`, `IEntityEnable`, `IEntityUpdate`, `IEntityDisable`).  
