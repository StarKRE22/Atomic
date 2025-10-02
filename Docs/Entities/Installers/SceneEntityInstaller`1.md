#  🧩 SceneEntityInstaller&lt;E&gt;

```csharp
public abstract class SceneEntityInstaller<E> : SceneEntityInstaller, IEntityInstaller<E> 
    where E : class, IEntity
```

- **Description:** Strongly-typed variant of `SceneEntityInstaller`
- **Type Parameter:** `E` – The specific type of [IEntity](../Entities/IEntity.md) this installer operates on.
- **Inheritance:** [SceneEntityInstaller](SceneEntityInstaller.md), [IEntityInstaller&lt;E&gt;](IEntityInstaller%601.md).
- **Notes:** Eliminates the need for manual casting in derived installer classes.
- **See also:** [ScriptableEntityInstaller&lt;E&gt;](ScriptableEntityInstaller%601.md)

---

## 🏹 Methods

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

## 🗂 Example of Usage

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