# 🧩 UnsafeAttribute

Marks an individual `EventKey<>` field in an `[EventExtensionsAPI]` class as unsafe. The source generator emits
`SubscribeUnsafe`, `UnsubscribeUnsafe`, and `InvokeUnsafe` calls for that field instead of the safe variants.

Use it to opt-in specific fields to unsafe calls when the class-level `Unsafe` flag is `false`, or to force safe
calls for a field when the class-level flag is `true`.

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

When the class-level `Unsafe` flag is `false` (the default), mark an event field with `[Unsafe]` to generate unsafe
methods for that field only:

```csharp
using Atomic.Events;

[EventExtensionsAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> GameStarted = new(nameof(GameStarted));          // safe

    [Unsafe]
    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));     // unsafe
}
```

Generated usage:

```csharp
bus.InvokeGameStarted();                                         // safe invoke
bus.InvokeDamageDealt(10);                                       // unsafe invoke
using var sub = bus.SubscribeDamageDealt(OnDamage);              // unsafe subscribe
bus.UnsubscribeDamageDealt(OnDamage);                            // unsafe unsubscribe
```

### Force Safe Mode

When the class-level `Unsafe` flag is `true`, use `[Unsafe(false)]` to keep a specific event safe:

```csharp
[EventExtensionsAPI(Unsafe = true)]
public static partial class MixedEventAPI
{
    [Unsafe(false)]
    public static readonly EventKey<IEventBus> GameStarted = new(nameof(GameStarted));          // safe

    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));     // unsafe
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
namespace Atomic.Events
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UnsafeAttribute : Attribute
    {
    }
}
```

- **Description:** Marks a single `EventKey<>` field for unsafe code generation.
- **Inheritance:** `Attribute`
- **Targets:** `AttributeTargets.Field`
- **Notes:**
  - This attribute is recognized only inside classes marked with `[EventExtensionsAPI]`.
  - It affects only `EventKey<>` fields.
  - The parameterless constructor means "force unsafe". There is no `Value` property; to force safe mode, use
    `[Unsafe(false)]`, which relies on the implicit `bool` conversion supported by the source generator parser.
  - Unsafe methods bypass runtime validation. Invoking an event with no subscribers, or subscribing/unsubscribing with
    an invalid state, can crash or leave the bus in an undefined state.
- **See also:** [EventExtensionsAPIAttribute](EventExtensionsAPIAttribute.md), [Event API Source Generation](../Manual.md#-event-api-source-generation)

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

- [EventExtensionsAPIAttribute](EventExtensionsAPIAttribute.md)
- [EventAPIAnalyzer](EventAPIAnalyzer.md)
- [Event API Source Generation](../Manual.md#-event-api-source-generation)
