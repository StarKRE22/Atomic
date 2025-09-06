# 🧩️ IEntityDisable

`IEntityDisable` is a behavior interface that executes logic when an `IEntity` **is disabled**.  
It is automatically invoked by the entity’s `Disable` method when the entity exits the active state, such as during pause, unloading, or before being despawned.

---

## Key Features

- **Disable-Time Logic** – Executes routines when an entity becomes inactive.
- **Strongly-Typed Option** – `IEntityDisable<E>` allows type-specific disable logic.
- **Automatic Invocation** – Called automatically by `IEntity.Disable`.
- **Composable** – Can be combined with other behaviours for modular entity logic.

---

## Interface: IEntityDisable

```csharp
public interface IEntityDisable : IEntityBehaviour
{
    void Disable(IEntity entity);
}
```
---
## Interface: IEntityDisable&lt;E&gt;
```csharp
public interface IEntityDisable<in E> : IEntityDisable where E : IEntity
{
    void Disable(E entity);
}
```
- Implements `IEntityDisable.Disable(IEntity)` automatically by casting to `E`.
- Ensures type-safe disable logic for specific entity types.
---
## Example Usage
Disable a `Renderer` component when an entity becomes inactive

### Example #1. Non-Generic (IEntity)
```csharp
public class DisableRendererBehaviour : IEntityDisable
{
    public void Disable(IEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        renderer.enabled = false;
    }
}
```

> Note: `GetValue<T>` assumes the entity has a `Renderer` component already set.

### Example #2. Generic with UnitEntity (strongly-typed)
```csharp
public class DisableRendererBehaviour : IEntityDisable<UnitEntity>
{
    public void Disable(UnitEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        renderer.enabled = false;
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.

## Remarks

- `IEntityDisable` is intended for logic that must run when an entity becomes inactive.
- `IEntityDisable<E>` is useful when the behaviour is specific to a particular entity type.
- Behaviours can interact with other entity behaviours during disabling.
- Does not handle initialization, enabling, updating, or disposal; separate interfaces exist for those phases (`IEntityInit`, `IEntityEnable`, `IEntityUpdate`, `IEntityDispose`).