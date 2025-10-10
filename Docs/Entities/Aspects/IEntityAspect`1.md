# 🧩 IEntityAspect&lt;E&gt;

Represents a generic interface that applies or discards reusable behavior on a strongly-typed entity.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Apply()](#applye)
        - [Discard()](#discarde)

---

## 🗂 Example of Usage

Adds jump-related capabilities to entities implementing `IGameEntity`:

```csharp
public interface IGameEntity : IEntity
{
}
```

```csharp
[Serializable]
public sealed class JumpAspect : IEntityAspect<IGameEntity>
{
    [SerializeField]
    private float _jumpForce = 2;

    public void Apply(IGameEntity entity)
    {
        entity.AddTag("Jumpable");
        entity.AddValue("JumpForce", _jumpForce);
        entity.AddBehaviour<JumpBehaviour>();
    }

    public void Discard(IGameEntity entity)
    {
        entity.DelTag("Jumpable");
        entity.DelValue("JumpForce");
        entity.DelBehaviour<JumpBehaviour>();
    }
}
```

> Note: Using the generic `IGameEntity` version allows type-safe access to entity-specific properties without casting.


---

## 🔍 API Reference

### 🏛️ Type

```csharp
public interface IEntityAspect<in E> where E : IEntity
```

- **Description:** Represents a generic interface that applies or discards reusable behavior on a strongly-typed entity.
- **Type Parameter:** `E` – The specific entity type this aspect operates on.
- **Note:** Provides type-safe application and discard of behavior for a specific entity type.
- **See also:** [IEntityAspect](IEntityAspect.md)

---

### 🏹 Methods

#### `Apply(E)`

```csharp
public void Apply(E entity);
```

- **Description:** Applies the aspect to the strongly-typed entity.
- **Parameter:** `entity` – The entity of type `E` to which the aspect will be applied.

#### `Discard(E)`

```csharp
public void Discard(E entity);
```

- **Description:** Reverses the effects of `Apply` on the strongly-typed entity.
- **Parameter:** `entity` – The entity of type `E` from which the aspect should be removed.