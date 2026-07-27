# 🧩 EntityInspectorAPIAttribute

**EntityInspectorAPIAttribute** marks a static class as an entity API definition so that the Unity Editor inspector
cache can discover its [TagKey](../KeyStore/TagKey.md) and [ValueKey](../KeyStore/ValueKey.md) fields. This enables
populated dropdowns for [EntityInspectorTagAttribute](EntityInspectorTagAttribute.md) and
[EntityInspectorValueAttribute](EntityInspectorValueAttribute.md).

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

When a static class is marked with `[EntityInspectorAPI]`, the editor scans its public static fields looking for:

- `TagKey` and `TagKey<E>` fields → registered as available tags
- `ValueKey<T>` and `ValueKey<E, T>` fields → registered as available values

These names are then offered as options in inspector popups drawn by `EntityInspectorTagAttribute` and
`EntityInspectorValueAttribute`.

> **Note:** This attribute and its drawers are available only in the Unity Editor and require **Odin Inspector**.

---

## 🗂 Examples of Usage

### Mark an API Class

```csharp
[EntityInspectorAPI]
public static partial class GameEntityAPI
{
    public static readonly TagKey IsEnemy = new(nameof(IsEnemy));
    public static readonly TagKey<IGameEntity> IsSelectable = new(nameof(IsSelectable));

    public static readonly ValueKey<IGameEntity, int> Health = new(nameof(Health));
    public static readonly ValueKey<IGameEntity, Vector3> Position = new(nameof(Position));
}
```

After marking the class, the fields appear in inspector dropdowns for matching entity and value types.

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[AttributeUsage(AttributeTargets.Class)]
public sealed class EntityInspectorAPIAttribute : Attribute
```

- **Targets:** Classes
- **Note:** Has no effect at runtime; used only by the editor inspector cache.

---

## 📌 Best Practices

- Apply `[EntityInspectorAPI]` to all static API classes that define `TagKey` or `ValueKey` fields you want exposed in the inspector.
- Keep API classes organized by entity type or feature area.
- Use partial classes to split generated and hand-written API definitions.
