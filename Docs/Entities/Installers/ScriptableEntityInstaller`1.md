# 🧩 ScriptableEntityInstaller&lt;E&gt;

Strongly-typed variant of [ScriptableEntityInstaller](ScriptableEntityInstaller.md).

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Install(E)](#installe)
        - [Uninstall(E)](#uninstalle)

---

## 🗂 Example of Usage

Assume we have a concrete type of entity:

```csharp
public class UnitEntity : Entity
{
}
```

Create a `ScriptableObject` installer for `UnitEntity`

```csharp
[CreateAssetMenu(
    fileName = "MoveInstaller",
    menuName = "Example/New MoveInstaller"
)]
public sealed class MoveInstaller : ScriptableEntityInstaller<UnitEntity>
{
    [SerializeField] private Const<float> _moveSpeed = 5.0f; 

    public override void Install(UnitEntity entity)
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
public abstract class ScriptableEntityInstaller<E> : ScriptableEntityInstaller, IEntityInstaller<E>
    where E : class, IEntity
```

- **Type Parameter:** `E` – The specific entity type this installer supports.
- **Inheritance:**
  [MonoEntityInstaller](MonoEntityInstaller.md), [IEntityInstaller&lt;E&gt;](IEntityInstaller%601.md).

---

### 🏹 Methods

#### `Install(E)`

```csharp
public abstract void Install(E entity);
```

- **Description:** Installs data, values, or behaviors into the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to install configuration or components into.
- **Remarks:** Must be implemented by derived classes.

#### `Uninstall(E)`

```csharp
public virtual void Uninstall(E entity)
```

- **Description:** Removes previously installed data or behavior from the strongly-typed entity.
- **Parameters:** `entity` – The entity of type `E` to uninstall configuration, components, or behavior from.
- **Remarks:** Default implementation does nothing. Override to provide custom uninstall behavior.