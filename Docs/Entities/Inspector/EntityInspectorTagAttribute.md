# 🧩 EntityInspectorTagAttribute

**EntityInspectorTagAttribute** is an Odin Inspector drawer attribute for `string` fields and parameters. It renders a
popup listing all available tag names discovered from classes marked with
[EntityInspectorAPIAttribute](EntityInspectorAPIAttribute.md).

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

Instead of typing tag names manually in the Inspector, this attribute draws a dropdown with all registered tags for the
specified entity type. The list is built automatically from `TagKey` and `TagKey<E>` fields found in API classes.

> **Note:** This attribute and its drawer are available only in the Unity Editor and require **Odin Inspector**.

---

## 🗂 Examples of Usage

### Field with Tag Dropdown

```csharp
public class DamageOnTagBehaviour : MonoBehaviour
{
    [EntityInspectorTag]
    [SerializeField]
    private string targetTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetEntity(out IEntity entity) && entity.HasTag(targetTag))
        {
            entity.TakeDamage(10);
        }
    }
}
```

### Typed Tag Dropdown

```csharp
public class GameEntityFilter : MonoBehaviour
{
    [EntityInspectorTag(typeof(IGameEntity))]
    [SerializeField]
    private string selectableTag;
}
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class EntityInspectorTagAttribute : Attribute
```

### 🏗️ Constructors

#### `EntityInspectorTagAttribute()`

```csharp
public EntityInspectorTagAttribute()
```

- **Description:** Uses `typeof(IEntity)` as the entity type.

#### `EntityInspectorTagAttribute(Type)`

```csharp
public EntityInspectorTagAttribute(Type entityType)
```

- **Description:** Specifies the entity type used to filter available tags.
- **Parameter:** `entityType` — The entity type whose registered tags should appear in the dropdown.

### 🏷️ Fields

| Field | Description |
|-------|-------------|
| `entityType` | The entity type used to filter tags. Defaults to `typeof(IEntity)`. |

---

## 📌 Best Practices

- Use `[EntityInspectorTag]` instead of plain `string` fields to avoid typos and improve discoverability.
- Specify a concrete entity type when the tag is only valid for that type.
- Ensure the tag is defined in a class marked with `[EntityInspectorAPI]` so it appears in the dropdown.
