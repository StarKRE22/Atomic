# 🧩️ IEntityDisable Interfaces

Represents a behavior interface that executes logic when an [IEntity](../Entities/IEntity.md) **is disabled**. It is
automatically invoked by the entity’s `Disable` method when the entity exits the active state, such as during pause,
unloading, or before being despawned.

<details>
  <summary>
    <h2 id="entity-disable"> 🧩 IEntityDisable</h2>
    <br>Defines a behavior that executes logic when an <code>IEntity</code> is disabled.
  </summary>

<br>

```csharp
public interface IEntityDisable : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)

---

### 🏹 Methods

#### `Disable(IEntity)`

```csharp
public void Disable(IEntity entity);
```

- **Description:** Called when the entity is disabled.
- **Parameter:** `entity` – The entity being disabled.
- **Remarks:** Automatically called by `IEntity.Disable` when the entity exits the active state.

---

### 🗂 Example of Usage

Disable a `Renderer` component

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

</details>

---

<details>
  <summary>
    <h2 id="entity-disable-t"> 🧩 IEntityDisable&lt;E&gt;</h2>
    <br>Defines a strongly-typed behavior that executes logic when an <code>IEntity</code> of type <code>E</code> is disabled.
  </summary>

<br>

```csharp
public interface IEntityDisable<in E> : IEntityDisable where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityDisable` for handling disable logic for a specific
  `IEntity` type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityDisable](#entity-disable)
- **Remarks:** Automatically invoked by `IEntity.Disable` when the behavior is registered on an entity of type `E`.

---

## 🏹 Methods

#### `Disable(E)`

```csharp
public void Disable(E entity);
```

- **Description:** Called when the typed entity is disabled.
- **Parameter:** `entity` – The entity instance of type `E`.
- **Remarks:** Implements the base `IEntityDisable.Disable(IEntity)` explicitly by casting the `IEntity` to type `E`.

---

### 🗂 Example of Usage

Disable a `Renderer` for a unit entity

```csharp
public class UnitEntity : Entity
{
}
```

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

</details>

---

## 📝 Notes

- **Disable Logic** – Encapsulates routines for when an entity becomes inactive.
- **Strongly-Typed Option** – `IEntityDisable<E>` allows type-specific disable logic.
- **Integration** – Called automatically by `IEntity.Disable`.
- **Composable** – Can be combined with other behaviours to form modular entity logic.

- `IEntityDisable` is intended for logic that must run when an entity becomes inactive.
- `IEntityDisable<E>` is useful when the behaviour is specific to a particular entity type.