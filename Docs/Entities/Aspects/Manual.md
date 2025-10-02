# 🧩 Entity Aspects

**Entity Aspect** is a component that applies or discards tags, values, and behaviors on an entity instance. It provides
a **declarative mechanism** for dynamically extending or modifying entities at runtime.

Below are the different types of aspects depending on the usage scenario:

- [IEntityAspect](IEntityAspect.md) <!-- + -->
- [IEntityAspect&lt;E&gt;](IEntityAspect%601.md) <!-- + -->
- [SceneEntityAspect](SceneEntityAspect.md) <!-- + -->
- [SceneEntityAspect&lt;E&gt;](SceneEntityAspect%601.md) <!-- + -->
- [ScriptableEntityAspect](ScriptableEntityAspect.md) <!-- + -->
- [ScriptableEntityAspect&lt;E&gt;](ScriptableEntityAspect%601.md) <!-- + -->

---

## 🗂 Example of Usage

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

## 📝 Notes

- **Aspect** — declarative way of extending entity behavior dynamically.
- **SceneEntityAspect** — implemented as `MonoBehaviour`, bound to the Unity scene.
- **ScriptableEntityAspect** — implemented as `ScriptableObject`, reusable and lightweight logic (buffs, debuffs, etc.).
- **Generic Aspects** — strongly typed variant (`IEntityAspect<E>`, `SceneEntityAspect<E>`, `ScriptableEntityAspect<E>`)
  for type safety and readability.
- Keep aspects focused on **behavior configuration only**; avoid embedding business logic.
- Always implement `Discard` to properly clean up (tags, values, behaviors) and prevent resource leaks.
- The same aspect instance can be applied to multiple entities for reusability.  
