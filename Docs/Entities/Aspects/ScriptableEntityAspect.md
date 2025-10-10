# 🧩 ScriptableEntityAspect

Represents a non-generic `ScriptableObject` that applies or discards reusable behavior on
any entity.

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

Below is an example of using aspect that temporarily increases an entity's damage:

```csharp
[CreateAssetMenu(
    fileName = "DamageBoost",
    menuName = "SampleGame/DamageBoost"
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

For example, you can use this aspect in a trigger that applies and discards the speed modifier when an entity enters or
exits the trigger:

```csharp
public class EntityAspectTrigger : MonoBehaviour
{
    [SerializeField]
    private ScriptableEntityAspect _aspect;
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out IEntity entity))
            _aspect.Apply(entity);
    }  
    
    private void OnTriggerExit(Collider collider)
    {
        if (collider.TryGetComponent(out IEntity entity))
           _aspect.Discard(entity);
    }
}
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
public abstract class ScriptableEntityAspect : ScriptableEntityAspect<IEntity>, IEntityAspect
```

- **Description:** Represents a non-generic `ScriptableObject` that applies or discards reusable behavior on
  any entity.
- **Inheritance:** [ScriptableEntityAspect&lt;E&gt;](ScriptableEntityAspect%601.md), [IEntityAspect](IEntityAspect.md)
- **Note:** Ideal for lightweight buffs or debuffs stored as `ScriptableObject` assets.

---

### 🏹 Methods

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