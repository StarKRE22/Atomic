# 🧩 UnsafeAttribute

Marks an individual `ValueKey<>` field in an `[EntityExtensionsAPI]` class as unsafe. The source generator emits
`GetValueUnsafe<T>` and `Ref{Name}()` methods for that field instead of the safe `GetValue<T>` accessor.

Use it to opt-in specific fields to unsafe access when the class-level `Unsafe` flag is `false`, or to force safe
access for a field when the class-level flag is `true`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
  - [Opt-in to Unsafe](#opt-in-to-unsafe)
  - [Force Safe Mode](#force-safe-mode)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [UnsafeAttribute()](#UnsafeAttribute)
- [See Also](#see-also)

---

## 🗂 Example of Usage

### Opt-in to Unsafe

When the class-level `Unsafe` flag is `false` (the default), mark a value field with `[Unsafe]` to generate unsafe
accessors for that field only:

```csharp
using Atomic.Entities;

[EntityExtensionsAPI]
public static partial class PlayerAPI
{
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));          // safe

    [Unsafe]
    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));          // unsafe
}
```

Generated usage:

```csharp
int health = entity.GetHealth();              // safe accessor with runtime checks
float speed = entity.GetSpeed();              // unsafe accessor, no runtime checks
ref float speedRef = ref entity.RefSpeed();   // direct reference to the stored value
```

### Force Safe Mode

When the class-level `Unsafe` flag is `true`, use `[Unsafe(false)]` to keep a specific field safe:

```csharp
[EntityExtensionsAPI(Unsafe = true)]
public static partial class MixedAPI
{
    [Unsafe(false)]
    public static readonly ValueKey<IEntity, int> Health = new(nameof(Health));          // safe

    public static readonly ValueKey<IEntity, float> Speed = new(nameof(Speed));          // unsafe
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
namespace Atomic.Entities
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UnsafeAttribute : Attribute
    {
    }
}
```

- **Description:** Marks a single `ValueKey<>` field for unsafe code generation.
- **Inheritance:** `Attribute`
- **Targets:** `AttributeTargets.Field`
- **Notes:**
  - This attribute is recognized only inside classes marked with `[EntityExtensionsAPI]`.
  - It affects only `ValueKey<>` fields. `TagKey<>` fields ignore this attribute.
  - The parameterless constructor means "force unsafe". There is no `Value` property; to force safe mode, use
    `[Unsafe(false)]`, which relies on the implicit `bool` conversion supported by the source generator parser.
  - Unsafe accessors bypass runtime existence checks. Calling them on an entity that does not contain the value can
    crash or return undefined data.
- **See also:** [EntityExtensionsAPIAttribute](EntityExtensionsAPIAttribute.md), [Entity API Source Generation](../Manual.md#-entity-api-source-generation)

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `UnsafeAttribute()`

```csharp
public UnsafeAttribute()
```

- **Description:** Initializes a new instance of the attribute, marking the field as unsafe.
- **Notes:** Equivalent to `[Unsafe(true)]` for parser purposes.

---

## See Also

- [EntityExtensionsAPIAttribute](EntityExtensionsAPIAttribute.md)
- [EntityAPIAnalyzer](EntityAPIAnalyzer.md)
- [Entity API Source Generation](../Manual.md#-entity-api-source-generation)
