# 🧩️ IEntityInit

`IEntityInit` is a behavior interface that executes logic when an `IEntity` is initialized.  
It is automatically invoked by the entity’s `Init` method during its transition to the initialized state.

---

## Key Features

- **Initialization Logic** – Encapsulates setup routines for entities.
- **Strongly-Typed Option** – `IEntityInit<T>` allows type-specific initialization.
- **Integration** – Called automatically by `IEntity.Init`.
- **Composable** – Can be combined with other behaviours to form modular entity logic.

---

## Interface: IEntityInit

```csharp
public interface IEntityInit : IEntityBehaviour
{
    void Init(IEntity entity);
}
```
---
## Interface: IEntityInit&lt;E&gt;

```csharp
public interface IEntityInit<in E> : IEntityInit where E : IEntity
{
    void Init(E entity);
}
```
- Implements `IEntityInit.Init(IEntity)` automatically by casting to `E`.
- Ensures type-safe initialization for specific entity types.

## Example Usage
Set up a `Color` for the entity `Renderer`

### Example #1. Non-Generic (IEntity)
```csharp
public class InitColorBehaviour : IEntityInit
{
    public void Init(IEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        var color = entity.GetValue<Color>("Color");
        renderer.material.color = color;
    }
}
```
> Note: `GetValue<T>` assumes the entity has these values already set.

### Example #2. Generic with UnitEntity (strongly-typed)

```csharp
public class InitColorBehaviour : IEntityInit<UnitEntity>
{
    public void Init(UnitEntity entity)
    {
        var renderer = entity.GetValue<Renderer>("Renderer");
        var color = entity.GetValue<Color>("Color");
        renderer.material.color = color;
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required
---

## Remarks

- `IEntityInit` is intended for setup routines that must run when an entity becomes initialized.
- `IEntityInit<E>` is useful when the behaviour is specific to a particular entity type.
- Behaviours can interact with other entity behaviours during initialization.
- Does not handle enabling, updating, or disposal; separate interfaces exist for those phases (`IEntityEnable`, `IEntityUpdate`, `IEntityDispose`).