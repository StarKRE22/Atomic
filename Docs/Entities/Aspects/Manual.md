# 🧩 Entity Aspects

**Entity Aspect** is a component that applies or discards tags, values, and behaviors on an entity instance. It provides
a **declarative mechanism** for dynamically extending or modifying entities at runtime.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
- [Notes](#-notes)

---

## 🗂 Example of Usage

Below is an example of creating a **speed buff** that extends [ScriptableEntityAspect](ScriptableEntityAspect.md) class:

```csharp
[CreateAssetMenu(
    fileName = "SpeedBuff",
    menuName = "SampleGame/SpeedBuff"
)]
public sealed class SpeedBuff : ScriptableEntityAspect
{
    [SerializeField]
    private float _factor = 2f;

    public override void Apply(IEntity entity)
    {
        IVariable<float> speed = entity.GetValue<IVariable<float>>("Speed"); 
        speed.Value *= _factor;
    }

    public override void Discard(IEntity entity)
    {
        IVariable<float> speed = entity.GetValue<IVariable<float>>("Speed"); 
        speed.Value /= _factor;
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

Below are the different types of aspects depending on the usage scenario:

- **Interfaces**
    - [IEntityAspect](IEntityAspect.md) <!-- + -->
    - [IEntityAspect&lt;E&gt;](IEntityAspect%601.md) <!-- + -->
- **MonoBehaviours**
    - [SceneEntityAspect](SceneEntityAspect.md) <!-- + -->
    - [SceneEntityAspect&lt;E&gt;](SceneEntityAspect%601.md) <!-- + -->
- **ScriptableObjects**
    - [ScriptableEntityAspect](ScriptableEntityAspect.md) <!-- + -->
    - [ScriptableEntityAspect&lt;E&gt;](ScriptableEntityAspect%601.md) <!-- + -->
- [Extensions](Extensions.md)

---

## 📝 Notes

- **Aspect** — declarative way of extending entity behavior dynamically.
- [SceneEntityAspect](SceneEntityAspect.md) — implemented as `MonoBehaviour`, bound to the Unity scene.
- [ScriptableEntityAspect](ScriptableEntityAspect.md) — implemented as `ScriptableObject`, reusable and lightweight
  logic (buffs, debuffs, etc.).
- **Generic Aspects:** [IEntityAspect\<E>](IEntityAspect%601.md), [SceneEntityAspect\<E>](SceneEntityAspect%601.md),
  [ScriptableEntityAspect\<E>](ScriptableEntityAspect%601.md) — strongly typed variant
  for type safety and readability.
- Keep aspects focused on **behavior configuration only**; avoid embedding business logic.
- Always implement `Discard` to properly clean up (tags, values, behaviors) and prevent resource leaks.
- **Use flyweight pattern** — the same aspect instance can be applied to multiple entities for reusability.  
