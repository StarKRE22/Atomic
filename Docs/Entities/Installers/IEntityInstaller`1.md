# 🧩 IEntityInstaller&lt;E&gt;

Provides a strongly-typed mechanism for installing entity configuration. Automatically implements the base
`Install(IEntity)` by casting the entity to `E`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Install(E)](#installe)

---

## 🗂 Example of Usage

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

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEntityInstaller<in E> : IEntityInstaller where E : IEntity
```

- **Type Parameter:** `E` – The specific entity type this installer targets.
- **Inheritance:** [IEntityInstaller](IEntityInstaller.md)

---

### 🏹 Methods

#### `Install(E)`

```csharp
public void Install(E entity);
```

- **Description:** Called when the typed entity is configured.
- **Parameter:** `entity` – The entity instance of type `E`.