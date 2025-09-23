# 🧩️ IEntityInit Interfaces

Represents a behavior interface that executes logic when an [IEntity](../Entities/IEntity.md) is initialized. It is
automatically invoked by the entity’s `Init` method during its transition to the initialized state.


<details>
  <summary>
    <h2 id="entity-init"> 🧩 IEntityInit</h2>
    <br>Defines a behavior that executes logic when an <code>IEntity</code> is initialized.
  </summary>

<br>

```csharp
public interface IEntityInit : IEntityBehaviour
```

- **Inheritance:** implements [IEntityBehaviour](IEntityBehaviour.md)

---

### 🏹 Methods

#### `Init(IEntity)`

```csharp
void Init(IEntity entity);
```

- **Description:** Called when the entity is initialized.
- **Parameter:** `entity` – The entity being initialized.
- **Remarks:** This method is automatically called by `IEntity.Init` when the entity transitions into its initialized
  state, such as after construction or deserialization.

---

### 🗂 Example of Usage

Set up a `Color` for the entity `Renderer`

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

</details>

---

<details>
  <summary>
    <h2 id="entity-init-t"> 🧩 IEntityInit&lt;E&gt;</h2>
    <br>Defines a behavior that executes logic when an <code>IEntity</code> is initialized.
  </summary>

<br>

```csharp
public interface IEntityInit<in E> : IEntityInit where E : IEntity
```

- **Description:** Provides a strongly-typed version of `IEntityInit` for handling initialization logic for a specific
  `IEntity` type.
- **Type Parameter:** `E` – The concrete entity type this behavior is associated with.
- **Inherits:** [IEntityInit](#entity-init)
- **Remarks:** This method is automatically invoked by `IEntity.Init` when the behavior is registered on an entity of
  type `E`.

---

## 🏹 Methods

#### `Init(E)`

```csharp
void Init(E entity);
```

- **Description:** Called when the typed entity is initialized.
- **Parameter:** `entity` – The entity instance of type `E`.
- **Remarks:** Implements the base `IEntityInit.Init(IEntity)` explicitly by casting the `IEntity` to type `E`.

---

### 🗂 Example of Usage

Set up a `Color` for the `Renderer` of unit entity

```csharp
public class UnitEntity : Entity
{
}
```

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

</details>

---

## 📝 Notes

- **Initialization Logic** – Encapsulates setup routines for entities.
- **Strongly-Typed Option** – `IEntityInit<T>` allows type-specific initialization.
- **Integration** – Called automatically by `IEntity.Init`.
- **Composable** – Can be combined with other behaviours to form modular entity logic.

- `IEntityInit` is intended for setup routines that must run when an entity becomes initialized.
- `IEntityInit<E>` is useful when the behaviour is specific to a particular entity type.
- Behaviours can interact with other entity behaviours during initialization.