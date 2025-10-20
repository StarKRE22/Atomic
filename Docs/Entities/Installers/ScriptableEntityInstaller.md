# 🧩 ScriptableEntityInstaller

Represents a Unity asset that defines **reusable logic for installing or configuring**
an [IEntity](../Entities/IEntity.md). Provides a reusable, declarative way to define entity setup logic that can be
shared across multiple
entities.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Install(IEntity)](#installientity)
        - [Uninstall(IEntity)](#uninstallientity)
---

## 🗂 Example of Usage

```csharp
[CreateAssetMenu(
    fileName = "MoveInstaller",
    menuName = "Example/New MoveInstaller"
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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class ScriptableEntityInstaller : ScriptableObject, IEntityInstaller
```

- **Inheritance:** `ScriptableObject`, [IEntityInstaller](IEntityInstaller.md)
- **See also:** [ScriptableEntityInstaller&lt;E&gt;](ScriptableEntityInstaller%601.md)

---

### 🏹 Methods

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