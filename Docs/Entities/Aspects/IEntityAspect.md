# 🧩 IEntityAspect

Represents an interface that applies or discards reusable behavior on any entity instance.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [Apply()](#applyientity)
    - [Discard()](#discardientity)

---

## 🗂 Example of Usage

Below an example of creating **speed buff** that applies a temporary speed nultiplier to an entity:

```csharp
[Serializable]
public sealed class SpeedBuff : IEntityAspect
{
    [SerializeField]
    private float _factor = 2f;

    public void Apply(IEntity entity)
    {
        IVariable<float> speed = entity.GetValue<IVariable<float>>("Speed"); 
        speed.Value *= _factor;
    }

    public void Discard(IEntity entity)
    {
        IVariable<float> speed = entity.GetValue<IVariable<float>>("Speed"); 
        speed.Value /= _factor;
    }
}
```

---

## 🔍 API Reference

<!--
Below an application programming interface of `IEntityAspect` including all containing methods 
-->

### 🏛️ Type

```csharp
public interface IEntityAspect : IEntityAspect<IEntity>
```

- **Description:** Represents an interface that applies or discards reusable behavior on any entity instance.
- **Note:** Allows modular behavior to be dynamically added or removed at runtime.
- **See also:** [IEntityAspect&lt;E&gt;](IEntityAspect%601.md)

---

### 🏹 Methods

#### `Apply(IEntity)`

```csharp
public void Apply(IEntity entity);
```

- **Description:** Applies the aspect to the specified entity.
- **Parameter:** `entity` – The entity to which the aspect will be applied.

#### `Discard(IEntity)`

```csharp
public void Discard(IEntity entity);
```

- **Description:** Reverses the effects of `Apply` on the specified entity.
- **Parameter:** `entity` – The entity from which the aspect should be removed.