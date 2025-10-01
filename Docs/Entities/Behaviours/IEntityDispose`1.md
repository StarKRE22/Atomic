#  🧩 IEntityDispose&lt;E&gt;

```csharp
public interface IEntityDispose<in E> : IEntityDispose where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityDispose` for handling disposal logic for a specific `IEntity` type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityDispose](IEntityDispose.md)
- **Note:** Automatically invoked by `IEntity.Dispose` when the behavior is registered on an entity of type `E`.

---

## 🏹 Methods

#### `Dispose(E)`

```csharp
public void Dispose(E entity);
```

- **Description:** Called when the typed entity is being disposed.
- **Parameter:** `entity` – The entity instance of type `E`.
- **Remarks:** Implements the base `IEntityDispose.Dispose(IEntity)` explicitly by casting the `IEntity` to type `E`.

---

## 🗂 Example of Usage

Dispose a `Collider` component

```csharp
public class UnitEntity : Entity
{
}
```

```csharp
public class DisposeColliderBehaviour : IEntityDispose<UnitEntity>
{
    public void Dispose(UnitEntity entity)
    {
        var collider = entity.GetValue<Collider>("Collider");
        Object.Destroy(collider);
    }
}
```

> Note: Uses the strongly-typed `UnitEntity`, so no casting from `IEntity` is required.
