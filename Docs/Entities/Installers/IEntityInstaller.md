# 🧩 IEntityInstaller Interfaces

Represents a interface that configures or injects data into an [IEntity](../Entities/IEntity.md) instance. It
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