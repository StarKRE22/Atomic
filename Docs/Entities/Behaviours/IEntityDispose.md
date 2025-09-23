# 🧩️ IEntityDispose Interfaces

Represents a behavior interface that executes cleanup or resource release logic when an [IEntity](../Entities/IEntity.md) **is being disposed**. It is automatically invoked by the entity’s `Dispose` method when the entity is permanently destroyed, removed from the game, or otherwise released from use.

---

<details>
  <summary>
    <h2 id="entity-dispose"> 🧩 IEntityDispose</h2>
    <br>Defines a behavior that executes logic when an <code>IEntity</code> is disposed.
  </summary>

<br>

```csharp
public interface IEntityDispose : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)

---

### 🏹 Methods

#### `Dispose(IEntity)`

```csharp
public void Dispose(IEntity entity);
```

- **Description:** Called when the entity is being disposed.
- **Parameter:** `entity` – The entity being disposed.
- **Remarks:** Automatically called by `IEntity.Dispose` when the entity is permanently removed or released.

---

### 🗂 Example of Usage

Dispose a `Collider` component

```csharp
public class DisposeColliderBehaviour : IEntityDispose
{
    public void Dispose(IEntity entity)
    {
        var collider = entity.GetValue<Collider>("Collider");
        Object.Destroy(collider);
    }
}
```

> Note: `GetValue<T>` assumes the entity has a `Collider` component already set.

</details>

---

<details>
  <summary>
    <h2 id="entity-dispose-t"> 🧩 IEntityDispose&lt;E&gt;</h2>
    <br>Defines a strongly-typed behavior that executes logic when an <code>IEntity</code> of type <code>E</code> is disposed.
  </summary>

<br>

```csharp
public interface IEntityDispose<in E> : IEntityDispose where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityDispose` for handling disposal logic for a specific `IEntity` type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityDispose](#entity-dispose)
- **Remarks:** Automatically invoked by `IEntity.Dispose` when the behavior is registered on an entity of type `E`.

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

### 🗂 Example of Usage

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

</details>

---

## 📝 Notes

- **Dispose Logic** – Encapsulates routines to clean up resources or release references when an entity is removed.
- **Strongly-Typed Option** – `IEntityDispose<E>` allows type-specific disposal logic.
- **Integration** – Called automatically by `IEntity.Dispose`.
- **Composable** – Can be combined with other behaviours to form modular entity logic.

- `IEntityDispose` is intended for logic that must run when an entity is permanently removed.
- `IEntityDispose<E>` is useful when the behaviour is specific to a particular entity type.