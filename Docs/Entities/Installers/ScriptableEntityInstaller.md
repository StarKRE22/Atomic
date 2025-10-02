# 🧩 ScriptableEntityInstaller

```csharp
public abstract class ScriptableEntityInstaller : ScriptableObject, IEntityInstaller
```
- **Description:** Represents a Unity asset that defines **reusable logic for installing or configuring**
an [IEntity](../Entities/IEntity.md). 
- **Inheritance:** `ScriptableObject`, [IEntityInstaller](IEntityInstaller.md)
- **Note:** Provides a reusable, declarative way to define entity setup logic that can be shared across multiple
  entities.
- **See also:** [ScriptableEntityInstaller&lt;E&gt;](ScriptableEntityInstaller%601.md)

---

## 🏹 Methods

#### `Install(IEntity)`

```csharp
public abstract void Install(IEntity entity);
```

- **Description:** Installs data, values, or behaviors into the specified entity.
- **Parameters:** `entity` – The entity to install configuration or components into.
- **Remarks:** Must be implemented by derived classes.

#### `Uninstall(IEntity)`

```csharp
public virtual void Uninstall(IEntity entity)
```

- **Description:** Optionally removes previously installed data or behavior from the specified entity.
- **Parameters:** `entity` – The entity to uninstall configuration, components, or behavior from.
- **Notes:** Default implementation does nothing. Override this method to provide custom uninstall logic.

---

## 🗂 Example of Usage

```csharp
[CreateAssetMenu(
    fileName = "MoveInstaller",
    menuName = "SampleGame/New MoveInstaller"
)]
public sealed class MoveInstaller : ScriptableEntityInstaller
{
    [SerializeField] private Const<float> _moveSpeed = 5.0f; 

    public override void Install(IEntity entity)
    {
        entity.AddTag("Moveable");
        entity.AddValue("MoveSpeed", _moveSpeed);
        entity.AddValue("MoveDirection", new ReactiveVariable<Vector3>());
    }
}
```