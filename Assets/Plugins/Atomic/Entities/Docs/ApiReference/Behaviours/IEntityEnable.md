# 🧩️ IEntityEnable

`IEntityEnable` is a behavior interface that executes logic when an `IEntity` **is enabled**.  
It is automatically invoked by the entity’s `Enable` method when the entity enters the active state, such as after spawning or resuming from a disabled state.

---

## Key Features

- **Enable-Time Logic** – Executes routines when an entity becomes active.
- **Strongly-Typed Option** – `IEntityEnable<E>` allows type-specific enabling logic.
- **Automatic Invocation** – Called automatically by `IEntity.Enable`.
- **Composable** – Can be combined with other behaviours for modular entity logic.

---

## Interface: IEntityEnable

```csharp
public interface IEntityEnable : IEntityBehaviour
{
    void Enable(IEntity entity);
}
```
---
## Interface: IEntityEnable&lt;E&gt;
```csharp
public interface IEntityEnable<in E> : IEntityEnable where E : IEntity
{
    void Enable(E entity);
}
```
- Implements `IEntityEnable.Enable(IEntity)` automatically by casting to `E`.
- Ensures type-safe enable logic for specific entity types.
---
## Example Usage
Enable a `Renderer` component when an entity becomes active

### Example #1. Non-Generic (IEntity)
```csharp
public class EnableRendererBehaviour : IEntityEnable
{
    public void Enable(IEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        renderer.enabled = true;
    }
}
```

> Note: `GetValue<T>` assumes the entity has a `Renderer` component already set.

### Example #2. Generic with UnitEntity (strongly-typed)
```csharp
public class EnableRendererBehaviour : IEntityEnable<UnitEntity>
{
    public void Enable(UnitEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        renderer.enabled = true;
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.

## Remarks
- `IEntityEnable` is intended for logic that must run when an entity becomes active.
- `IEntityEnable<E>` is useful when the behaviour is specific to a particular entity type.
- Behaviours can interact with other entity behaviours during enabling.
- Does not handle initialization, updating, or disposal; separate interfaces exist for those phases (`IEntityInit`, `IEntityUpdate`, `IEntityDispose`).