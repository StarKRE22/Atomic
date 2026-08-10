# 🧩 EventExtensionsAPIAttribute

Marks a static class as an **Event API definition** for the Event API Generator. The generator reads `EventKey<>` fields
and emits strongly-typed extension methods for subscribing, invoking, and unsubscribing from events.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
  - [Unsafe Mode](#unsafe-mode)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [EventExtensionsAPIAttribute()](#EventExtensionsAPIAttribute)
  - [Properties](#-properties)
    - [Unsafe](#unsafe)

---

## 🗂 Example of Usage

Define event keys in a `public static partial` class:

```csharp
using Atomic.Events;

[EventExtensionsAPI]
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

### Unsafe Mode

Enable unsafe calls for every event field in the class:

```csharp
[EventExtensionsAPI(Unsafe = true)]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> GameStarted = new(nameof(GameStarted));
    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));
}
```

Generated methods skip runtime validation:

```csharp
bus.InvokeGameStarted();                            // calls InvokeUnsafe
using var sub = bus.SubscribeDamageDealt(OnDamage); // calls SubscribeUnsafe
bus.UnsubscribeDamageDealt(OnDamage);               // calls UnsubscribeUnsafe
```

Mix modes by applying `[Unsafe]` to individual fields:

```csharp
[EventExtensionsAPI]
public static partial class MixedEventAPI
{
    public static readonly EventKey<IEventBus> GameStarted = new(nameof(GameStarted));          // safe

    [Unsafe]
    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));     // unsafe

    [Unsafe(false)]
    public static readonly EventKey<IEventBus> GameEnded = new(nameof(GameEnded));              // safe override
}
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class EventExtensionsAPIAttribute : Attribute
```

- **Description:** Marks a static class as an Event API definition for source generation.
- **Inheritance:** `Attribute`
- **Notes:**
  - The target class must be `public static partial`.
  - The generator reads static fields of type [EventKey&lt;TBus&gt;](../Keys/EventKey.md) from the `Atomic.Events`
    namespace.
  - Supported shapes: `EventKey<TBus>`, `EventKey<TBus, T>`, `EventKey<TBus, T1, T2>`, `EventKey<TBus, T1, T2, T3>`.
  - Generated methods include `Subscribe{Name}`, `Unsubscribe{Name}`, `Invoke{Name}`, `IsSubscribed{Name}`, and
    `Dispose{Name}`.
- **See also:** [UnsafeAttribute](UnsafeAttribute.md), [EventAPIAnalyzer](EventAPIAnalyzer.md), [Event API Source Generation](../Manual.md#-event-api-source-generation)

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `EventExtensionsAPIAttribute()`

```csharp
public EventExtensionsAPIAttribute()
```

- **Description:** Initializes a new instance of the attribute.

---

### 🔑 Properties

#### `Unsafe`

```csharp
public bool Unsafe { get; set; }
```

- **Description:** Gets or sets whether generated event methods use unsafe direct access.
- **Access:** Read-write
- **Notes:**
  - When `true`, all `EventKey<>` fields in the class emit `SubscribeUnsafe`, `UnsubscribeUnsafe`, and `InvokeUnsafe`
    calls instead of the safe `Subscribe`/`Unsubscribe`/`Invoke` variants.
  - The class-level default can be overridden per field with `[Unsafe]` (force unsafe) or `[Unsafe(false)]` (force safe).
  - Unsafe methods skip runtime validation. Invoking an event that has no subscribers, or subscribing/unsubscribing with
    an invalid key, can crash or leave the bus in an undefined state.
  - Only enable this flag in performance-critical code paths where the event state is guaranteed.
