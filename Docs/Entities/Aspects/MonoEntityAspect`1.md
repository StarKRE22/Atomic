# 🧩 MonoEntityAspect&lt;E&gt;

Represents a generic `MonoBehaviour` that applies or discards reusable behavior on a strongly-typed
entity.

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

Below is an example of using aspect that adds jump capabilities to entities implementing `IGameEntity`:

```csharp
public interface IGameEntity : IEntity
{
}
```

```csharp
public sealed class JumpAspect : MonoEntityAspect<IGameEntity>
{
    [SerializeField]
    private float _jumpForce = 3f;

    public override void Apply(IGameEntity entity)
    {
        entity.AddTag("Jumpable");
        entity.AddValue("JumpForce", _jumpForce);
        entity.AddBehaviour<JumpBehaviour>();
    }

    public override void Discard(IGameEntity entity)
    {
        entity.DelTag("Jumpable");
        entity.DelValue("JumpForce");
        entity.DelBehaviour<JumpBehaviour>();
    }
}
```

> Note: Using the generic `MonoEntityAspect<IGameEntity>` allows type-safe access to entity-specific properties without
> casting.

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public abstract class MonoEntityAspect<E> : MonoBehaviour, IEntityAspect<E> where E : IEntity
```

- **Description:** Represents a generic `MonoBehaviour` that applies or discards reusable behavior on a strongly-typed
  entity.
- **Type Parameter:** `E` – The specific entity type this aspect operates on.
- **Inheritance:** [IEntityAspect&lt;E&gt;](IEntityAspect%601.md)
- **Note:** Provides type-safe application and discard for a specific entity type.
- **See also:** [MonoEntityAspect](MonoEntityAspect.md)

---

### 🏹 Methods

#### `Apply(E)`

```csharp
public abstract void Apply(E entity);
```

- **Description:** Applies the aspect to the strongly-typed entity.
- **Parameter:** `entity` – The entity of type `E` to which the aspect will be applied.

#### `Discard(E)`

```csharp
public abstract void Discard(E entity);
```

- **Description:** Reverses the effects of `Apply` on the strongly-typed entity.
- **Parameter:** `entity` – The entity from which the aspect should be removed.