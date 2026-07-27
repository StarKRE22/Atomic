# 🧩 EntityInspectorAPIAttribute

Marks a static class as an entity API definition so that the Unity Editor inspector cache can discover its [TagKey](../KeyStore/TagKey.md) and [ValueKey](../KeyStore/ValueKey.md) fields.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)

---

## 🗂 Example of Usage

Mark a static API class to expose its tag and value keys in inspector dropdowns:

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

The fields are then offered as options by [EntityInspectorTagAttribute](EntityInspectorTagAttribute.md) and [EntityInspectorValueAttribute](EntityInspectorValueAttribute.md).

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[AttributeUsage(AttributeTargets.Class)]
public sealed class EntityInspectorAPIAttribute : Attribute
```

- **Description:** Marks a static class as an entity API definition so that the Unity Editor inspector cache can discover its [TagKey](../KeyStore/TagKey.md) and [ValueKey](../KeyStore/ValueKey.md) fields.
- **Inheritance:** `Attribute`
- **Targets:** Classes
- **Notes:** Has no effect at runtime; used only by the editor inspector cache. Requires Odin Inspector.
- **See also:** [EntityInspectorTagAttribute](EntityInspectorTagAttribute.md), [EntityInspectorValueAttribute](EntityInspectorValueAttribute.md), [TagKey](../KeyStore/TagKey.md), [ValueKey](../KeyStore/ValueKey.md)
