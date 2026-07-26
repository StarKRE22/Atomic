# 🧩 MonoEntityInstaller&lt;E&gt;

Strongly-typed variant of [MonoEntityInstaller](MonoEntityInstaller.md). Eliminates the need for manual casting in
derived installer classes.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Install(E)](#installe)
        - [Uninstall(E)](#uninstalle)
        - [OnValidate()](#onvalidate)

---

## 🗂 Example of Usage

Assume we have a concrete type of entity

```csharp
public sealed class UnitEntity : SceneEntity
{
}
```

Create a `MonoBehaviour` installer for `UnitEntity`

```csharp
public sealed class CharacterInstaller : MonoEntityInstaller<UnitEntity>
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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class MonoEntityInstaller<E> : MonoEntityInstaller, IEntityInstaller<E> 
    where E : class, IEntity
```

- **Type Parameter:** `E` – The specific type of [IEntity](../Entities/IEntity.md) this installer operates on.
- **Inheritance:
  ** [MonoEntityInstaller](MonoEntityInstaller.md), [IEntityInstaller&lt;E&gt;](IEntityInstaller%601.md).
- **See also:** [ScriptableEntityInstaller&lt;E&gt;](ScriptableEntityInstaller%601.md)

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