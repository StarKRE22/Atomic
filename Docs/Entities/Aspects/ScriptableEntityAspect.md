# 🧩 ScriptableEntityAspect

```csharp
public abstract class ScriptableEntityAspect : ScriptableEntityAspect<IEntity>, IEntityAspect
```

- **Description:** Represents a non-generic `ScriptableObject` that applies or discards reusable behavior on
  any [IEntity](../Entities/IEntity.md).
- **Inheritance:** [ScriptableEntityAspect&lt;E&gt;](SceneEntityAspect%601.md), [IEntityAspect](IEntityAspect.md)
- **Note:** Ideal for lightweight buffs or debuffs stored as `ScriptableObject` assets.

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

Temporarily increases an entity's damage:

```csharp
[CreateAssetMenu(
    fileName = "DamageBoost",
    menuName = "SampleGame/New DamageBoost"
)]
public sealed class DamageBoost : ScriptableEntityAspect
{
    [SerializeField]
    private float _bonusDamage = 50f;

    public override void Apply(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Damage").Value += _bonusDamage;
    }

    public override void Discard(IEntity entity)
    {
        entity.GetValue<IVariable<float>>("Damage").Value -= _bonusDamage;
    }
}
```