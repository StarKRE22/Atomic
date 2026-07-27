# 🧩 Inspector Attributes

**Inspector attributes** provide enhanced editing workflows in the Unity Editor when using Odin Inspector. They allow
tag and value names defined in API classes to appear as dropdowns, reducing typos and improving discoverability.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Attributes](#-attributes)
- [Examples of Usage](#-examples-of-usage)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

The framework supports strongly-typed [TagKey](../KeyStore/TagKey.md) and [ValueKey](../KeyStore/ValueKey.md) objects, but
sometimes behaviours or installers need to reference a tag or value by its string name (for example, when a value type is
chosen at runtime). Inspector attributes bridge this gap by scanning API classes and offering the known names as dropdowns
in the Inspector.

---

## 🔍 Attributes

| Attribute | Purpose |
|-----------|---------|
| [EntityInspectorAPIAttribute](EntityInspectorAPIAttribute.md) | Marks a static class as an API definition to be scanned for tag and value keys. |
| [EntityInspectorTagAttribute](EntityInspectorTagAttribute.md) | Draws a dropdown of available tag names for a `string` field. |
| [EntityInspectorValueAttribute](EntityInspectorValueAttribute.md) | Draws a dropdown of available value names for a `string` field, filtered by value type. |

---

## 🗂 Examples of Usage

### Define API Keys

```csharp
[EntityInspectorAPI]
public static partial class GameEntityAPI
{
    public static readonly TagKey IsEnemy = new(nameof(IsEnemy));
    public static readonly ValueKey<IGameEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IGameEntity, float> MoveSpeed = new(nameof(MoveSpeed));
}
```

### Use Inspector Dropdowns

```csharp
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

---

## 📌 Best Practices

- Mark all API classes containing `TagKey` / `ValueKey` fields with `[EntityInspectorAPI]`.
- Prefer `[EntityInspectorTag]` and `[EntityInspectorValue]` over plain `string` fields in behaviours and installers.
- Specify concrete entity types when the dropdown should be filtered to a specific entity interface.
- These attributes are editor-only; always validate string names at runtime if they can be supplied outside the Inspector.
