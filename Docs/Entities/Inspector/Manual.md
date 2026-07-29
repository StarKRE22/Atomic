# 🧩 Inspector Attributes

**Inspector attributes** provide enhanced editing workflows in the Unity Editor when using Odin Inspector. They allow
tag and value names defined in API classes to appear as dropdowns, reducing typos and improving discoverability.

The framework works without Odin, but these attributes are editor-only conveniences for teams that use it.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Mark an API Class](#mark-an-api-class)
  - [Use Dropdowns in Behaviours](#use-dropdowns-in-behaviours)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### Mark an API Class

Add `[EntityInspectorAPI]` to any static class that contains `TagKey` or `ValueKey` definitions. Odin will scan the
class and offer known names as dropdowns.

```csharp
using Atomic.Entities;

[EntityInspectorAPI]
public static partial class GameEntityAPI
{
    public static readonly TagKey IsEnemy = new(nameof(IsEnemy));
    public static readonly ValueKey<IGameEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IGameEntity, float> MoveSpeed = new(nameof(MoveSpeed));
}
```

### Use Dropdowns in Behaviours

```csharp
using Atomic.Entities;
using UnityEngine;

public class DamageBehaviour : MonoBehaviour
{
    [EntityInspectorTag]
    [SerializeField]
    private string targetTag;

    [EntityInspectorValue(typeof(int))]
    [SerializeField]
    private string healthValueName;

    public void ApplyDamage(IEntity entity)
    {
        if (!entity.HasTag(targetTag))
            return;

        int health = entity.GetValue<int>(healthValueName);
        entity.SetValue(healthValueName, health - 10);
    }
}
```

To filter dropdowns for a specific entity type, pass the entity type to the attribute:

```csharp
[EntityInspectorTag(typeof(IGameEntity))]
[SerializeField]
private string gameEntityTag;
```

---

## 🔍 API Reference

- [EntityInspectorAPIAttribute](EntityInspectorAPIAttribute.md) — marks a static class as an API definition to be scanned
- [EntityInspectorTagAttribute](EntityInspectorTagAttribute.md) — draws a dropdown of available tag names
- [EntityInspectorValueAttribute](EntityInspectorValueAttribute.md) — draws a dropdown of available value names

---

## 📌 Best Practices

- Mark all API classes containing `TagKey` / `ValueKey` fields with `[EntityInspectorAPI]`.
- Prefer `[EntityInspectorTag]` and `[EntityInspectorValue]` over plain `string` fields in behaviours and installers.
- Specify concrete entity types when the dropdown should be filtered to a specific entity interface.
- These attributes are editor-only; always validate string names at runtime if they can be supplied outside the Inspector.
