# 🧩 IEntityInstaller Interfaces

Represents a behavior interface that configures or injects data into an [IEntity](../Entities/IEntity.md) instance. It
allows setting up values, components, or behaviors during initialization or configuration phases.

---

<details>
  <summary>
    <h2 id="entity-installer"> 🧩 IEntityInstaller</h2>
    <br>Defines a behavior that installs configuration into an <code>IEntity</code>.
  </summary>

<br>

```csharp
public interface IEntityInstaller
```

- **Description:** Provides a generic mechanism for configuring an entity.
- **Use Case:** Add tags, values, or behaviors to an entity during its setup phase.

---

### 🏹 Methods

#### `Install(IEntity)`

```csharp
public void Install(IEntity entity);
```

- **Description:** Called to install data or behaviors into the entity.
- **Parameter:** `entity` – The entity being configured.
- **Remarks:** Provides a flexible, non-generic entry point for entity configuration.

---

### 🗂 Example of Usage

Add tags, values, and behaviors to a character entity:

```csharp
[Serializable]
public sealed class CharacterInstaller : IEntityInstaller
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private Vector3 _moveDirection;

    public void Install(IEntity entity)
    {
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);
        
        entity.AddBehaviour<MoveBehaviour>();
        entity.AddBehaviour<LookBehaviour>();
    }

}
```

</details>

---

<details>
  <summary>
    <h2 id="entity-installer-t"> 🧩 IEntityInstaller&lt;E&gt;</h2>
    <br>Defines a strongly-typed behavior that installs configuration into an <code>IEntity</code> of type <code>E</code>.
  </summary>

<br>

```csharp
public interface IEntityInstaller<in E> : IEntityInstaller where E : IEntity
```

- **Description:** Provides a strongly-typed mechanism for installing entity configuration.
- **Type Parameter:** `E` – The specific entity type this installer targets.
- **Inherits:** [IEntityInstaller](#entity-installer)
- **Remarks:** Automatically implements the base `Install(IEntity)` by casting the entity to `E`.

### 🏹 Methods

#### `Install(E)`

```csharp
public void Install(E entity);
```

- **Description:** Called when the typed entity is configured.
- **Parameter:** `entity` – The entity instance of type `E`.

---

### 🗂 Example of Usage

Strongly-typed installer for `ICharacterEntity`:

```csharp
public interface ICharacterEntity : IEntity
{
}
```

```csharp
[Serializable]
public sealed class CharacterInstaller : IEntityInstaller<ICharacterEntity>
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private Vector3 _moveDirection;

    public void Install(ICharacterEntity entity)
    {
        entity.AddTag("Character");
        entity.AddTag("Moveable");

        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", _moveDirection);
        
        entity.AddBehaviour<MoveBehaviour>();
        entity.AddBehaviour<LookBehaviour>();
    }
}
```

</details>

---

## 📝 Notes

- **Entity Configuration** – Encapsulates setup routines for entities.
- **Strongly-Typed Option** – `IEntityInstaller<E>` allows type-safe configuration.
- **Composable** – Multiple installers can be applied to the same entity.
- **Integration** – Works in both runtime and editor simulation workflows.
- `IEntityInstaller` is intended for configuring or initializing entities before or during their lifecycle.
- `IEntityInstaller<E>` is useful when the installer is specific to a particular entity type.