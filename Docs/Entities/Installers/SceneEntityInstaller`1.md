<details>
  <summary>
    <h2 id="scene-entity-installer-t"> 🧩 SceneEntityInstaller&lt;E&gt;</h2>
    <br>Strongly-typed variant of <code>SceneEntityInstaller</code>.
  </summary>

<br>

```csharp
public abstract class SceneEntityInstaller<E> : SceneEntityInstaller, IEntityInstaller<E> 
    where E : class, IEntity
```

- **Type Parameter:** `E` – The specific type of `IEntity` this installer operates on.
- **Inheritance:** Inherits from [SceneEntityInstaller](#scene-entity-installer) and
  implements [IEntityInstaller&lt;E&gt;](IEntityInstaller.md/#entity-installer-t).
- **Notes:** Eliminates the need for manual casting in derived installer classes.

---

### 🏹 Methods

#### `Install(E entity)`

```csharp
public abstract void Install(E entity);
```

- **Description:** Installs data, values, or behaviors into the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to install configuration or components into.
- **Remarks:** Must be implemented by derived classes.

#### `Uninstall(E entity)`

```csharp
public virtual void Uninstall(E entity);
```

- **Description:** Removes previously installed data or behavior from the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to uninstall configuration, components, or behavior from.
- **Remarks:** Default implementation does nothing. Override to provide custom uninstall behavior.

#### `OnValidate()`

```csharp
protected virtual void OnValidate();
```

- **Description:** Called by Unity when the component is modified in the Inspector.
- **Note:** Runs only in the Unity Editor; does not execute at runtime.

---

### 🗂 Example of Usage

```csharp
public sealed class UnitEntity : SceneEntity
{
}
```

```csharp
public sealed class CharacterInstaller : SceneEntityInstaller<UnitEntity>
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 5.0f;

    protected override void Install(UnitEntity entity)
    {
        entity.AddTag("Character");
        entity.AddTag("Moveable");
        
        entity.AddValue("Transform", _transform);
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", Vector3.zero);
        
        entity.AddBehaviour<MoveBehaviour>();
    }
}
```

> Note: Using the generic `UnitEntity` version allows type-safe access to entity-specific properties without casting.

</details>