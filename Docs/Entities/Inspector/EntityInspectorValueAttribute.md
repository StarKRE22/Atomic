# 🧩 EntityInspectorValueAttribute

**EntityInspectorValueAttribute** is an Odin Inspector drawer attribute for `string` fields and parameters. It renders a
popup listing all available value names discovered from classes marked with
[EntityInspectorAPIAttribute](EntityInspectorAPIAttribute.md), filtered by value type.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

Instead of typing value names manually in the Inspector, this attribute draws a dropdown with all registered values of
the specified type. The list is built automatically from `ValueKey<T>` and `ValueKey<E, T>` fields found in API classes.

> **Note:** This attribute and its drawer are available only in the Unity Editor and require **Odin Inspector**.

---

## 🗂 Examples of Usage

### Field with Value Dropdown

```csharp
public class HealthBonusBehaviour : MonoBehaviour
{
    [EntityInspectorValue(typeof(int))]
    [SerializeField]
    private string healthValueName;

    public void ApplyBonus(IEntity entity)
    {
        int currentHealth = entity.GetValue<int>(healthValueName);
        entity.SetValue(healthValueName, currentHealth + 10);
    }
}
```

### Typed Entity and Value Dropdown

```csharp
public class MoveSpeedBonus : MonoBehaviour
{
    [EntityInspectorValue(typeof(IGameEntity), typeof(float))]
    [SerializeField]
    private string moveSpeedValueName;
}
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class EntityInspectorValueAttribute : Attribute
```

### 🏗️ Constructors

#### `EntityInspectorValueAttribute(Type)`

```csharp
public EntityInspectorValueAttribute(Type valueType)
```

- **Description:** Uses `typeof(IEntity)` as the entity type and the specified value type.
- **Parameter:** `valueType` — The type of the value whose keys should appear in the dropdown.

#### `EntityInspectorValueAttribute(Type, Type)`

```csharp
public EntityInspectorValueAttribute(Type entityType, Type valueType)
```

- **Description:** Specifies both the entity type and the value type.
- **Parameter:** `entityType` — The entity type used to filter values.
- **Parameter:** `valueType` — The type of the value whose keys should appear in the dropdown.

### 🏷️ Fields

| Field | Description |
|-------|-------------|
| `entityType` | The entity type used to filter values. Defaults to `typeof(IEntity)`. |
| `valueType` | The value type used to filter keys. |

---

## 📌 Best Practices

- Use `[EntityInspectorValue]` instead of plain `string` fields to avoid typos and improve discoverability.
- Specify the exact value type to narrow the dropdown to relevant keys.
- Ensure the value is defined in a class marked with `[EntityInspectorAPI]` so it appears in the dropdown.
- Combine with `[EntityInspectorTag]` when a behaviour needs both tag and value references.
