# 🧩 SceneEntityAspect

```csharp
public abstract class SceneEntityAspect : SceneEntityAspect<IEntity>, IEntityAspect
```

- **Description:** Represents a non-generic `MonoBehaviour` that applies or discards reusable behavior on any [IEntity](../Entities/IEntity.md) within a Unity scene.
- **Inheritance:** [SceneEntityAspect&lt;E&gt;](SceneEntityAspect%601.md)
- **Note:** Ideal for modular behaviors that can be dynamically applied or removed at runtime.

---

## 🏹 Methods

#### `Apply(IEntity)`

```csharp
public abstract void Apply(IEntity entity);
```

- **Description:** Applies the aspect to the specified entity.
- **Parameter:** `entity` – The entity to which the aspect will be applied.

#### `Discard(IEntity)`

```csharp
public abstract void Discard(IEntity entity);
```

- **Description:** Reverses the effects of `Apply` on the specified entity.
- **Parameter:** `entity` – The entity from which the aspect should be removed.

---

## 🗂 Example of Usage

Temporarily multiplies an entity's speed and restores it when discarded:

```csharp
public sealed class SpeedBoost : SceneEntityAspect
{
    [SerializeField]
    private float _multiplier = 1.5f;

    public override void Apply(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value *= _multiplier;
    }

    public override void Discard(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Speed").Value /= _multiplier;
    }
}
```