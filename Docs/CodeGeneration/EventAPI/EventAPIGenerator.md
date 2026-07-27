# 🧬 GenerateEventExtensionsAPIAttribute

Marks a static class as an **Event API definition** for the Event API Generator. The generator reads `EventKey<>` fields
and emits strongly-typed extension methods for subscribing, invoking, and unsubscribing from events.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [GenerateEventExtensionsAPIAttribute()](#GenerateEventExtensionsAPIAttribute)

---

## 🗂 Example of Usage

Define event keys in a `public static partial` class:

```csharp
using Atomic.Events;

[GenerateEventExtensionsAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));
    public static readonly EventKey<IEventBus, IEntity, int> EntityHealed = new(nameof(EntityHealed));
}
```

After compilation, use the generated extension methods:

```csharp
IEventBus bus = new EventBus();

bus.InvokePlayerTurnStarted();
bus.InvokeDamageDealt(10);
bus.InvokeEntityHealed(entity, 25);

using var subscription = bus.SubscribeDamageDealt(amount => Debug.Log($"Damage: {amount}"));
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class GenerateEventExtensionsAPIAttribute : Attribute
```

- **Description:** Marks a static class as an Event API definition for source generation.
- **Inheritance:** `Attribute`
- **Notes:**
  - The target class must be `public static partial`.
  - The generator reads static fields of type [EventKey&lt;TBus&gt;](../../Events/Keys/EventKey.md) from the `Atomic.Events`
    namespace.
  - Supported shapes: `EventKey<TBus>`, `EventKey<TBus, T>`, `EventKey<TBus, T1, T2>`, `EventKey<TBus, T1, T2, T3>`.
  - Generated methods include `Subscribe{Name}`, `Unsubscribe{Name}`, `Invoke{Name}`, `IsSubscribed{Name}`, and
    `Dispose{Name}`.
- **See also:** [EventAPIAnalyzer](EventAPIAnalyzer.md), [Setup](../Setup.md), [Code Generation Manual](../Manual.md)

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `GenerateEventExtensionsAPIAttribute()`

```csharp
public GenerateEventExtensionsAPIAttribute()
```

- **Description:** Initializes a new instance of the attribute.
