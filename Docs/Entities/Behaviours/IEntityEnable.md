# 🧩️ IEntityEnable Interfaces

Represents a behavior interface that executes logic when an [IEntity](../Entities/IEntity.md) **is enabled**. It is automatically invoked by the entity’s `Enable` method during its transition to the active state, such as after spawning or resuming from a disabled state.

---

<details>
  <summary>
    <h2 id="entity-enable"> 🧩 IEntityEnable</h2>
    <br>Defines a behavior that executes logic when an <code>IEntity</code> is enabled.
  </summary>

<br>

```csharp
public interface IEntityEnable : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)

---

### 🏹 Methods

#### `Enable(IEntity)`

```csharp
void Enable(IEntity entity);
```

- **Description:** Called when the entity is enabled.
- **Parameter:** `entity` – The entity being enabled.
- **Remarks:** This method is automatically called by `IEntity.Enable` when the entity transitions into its active state.

---

### 🗂 Example of Usage

Enable a `Renderer` component

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

</details>

---

<details>
  <summary>
    <h2 id="entity-enable-t"> 🧩 IEntityEnable&lt;E&gt;</h2>
    <br>Defines a strongly-typed behavior that executes logic when an <code>IEntity</code> of type <code>E</code> is enabled.
  </summary>

<br>

```csharp
public interface IEntityEnable<in E> : IEntityEnable where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityEnable` for handling enable logic for a specific `IEntity` type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityEnable](#entity-enable)
- **Remarks:** Automatically invoked by `IEntity.Enable` when the behavior is registered on an entity of type `E`.

---

## 🏹 Methods

#### `Enable(E)`

```csharp
void Enable(E entity);
```

- **Description:** Called when the typed entity is enabled.
- **Parameter:** `entity` – The entity instance of type `E`.
- **Remarks:** Implements the base `IEntityEnable.Enable(IEntity)` explicitly by casting the `IEntity` to type `E`.

---

### 🗂 Example of Usage

Enable a `Renderer` for a unit entity

```csharp
public class UnitEntity : Entity
{
}
```

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

</details>

---

## 📝 Notes

- **Enable Logic** – Encapsulates routines for when an entity becomes active.
- **Strongly-Typed Option** – `IEntityEnable<E>` allows type-specific enabling logic.
- **Integration** – Called automatically by `IEntity.Enable`.
- **Composable** – Can be combined with other behaviours to form modular entity logic.

- `IEntityEnable` is intended for logic that must run when an entity becomes active.
- `IEntityEnable<E>` is useful when the behaviour is specific to a particular entity type.