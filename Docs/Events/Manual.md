# 🧩 Atomic.Events

**Atomic.Events** provides a lightweight, strongly-typed event bus system for Unity and C#. It supports parameterless
and parameterized events, subscriptions, thread-safe dispatch, and Unity scene-bound buses.

The event system decouples publishers from subscribers using integer event keys wrapped in strongly-typed
`EventKey<TBus>` structs.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Define Events with Source Generation](#define-events-with-source-generation)
  - [Subscribe and Invoke](#subscribe-and-invoke)
  - [Thread-Safe Dispatch](#thread-safe-dispatch)
- [Event API Source Generation](#-event-api-source-generation)
  - [Unsafe Mode](#unsafe-mode)
  - [Analyzer](#analyzer)
  - [Setup](#setup)
- [API Reference](#-api-reference)
  - [Bus Implementations](#bus-implementations)
  - [Event Keys](#event-keys)
  - [Subscriptions](#subscriptions)
  - [Extensions](#extensions)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### Define Events with Source Generation

```csharp
using Atomic.Events;

[EventExtensionsAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));
    public static readonly EventKey<IEventBus, IGameEntity> EntityDied = new(nameof(EntityDied));
}
```

### Subscribe and Invoke

```csharp
IEventBus eventBus = new EventBus();

using var subscription = eventBus.SubscribeEntityDied(entity =>
{
    Debug.Log($"Entity died: {entity}");
});

eventBus.InvokePlayerTurnStarted();
eventBus.InvokeDamageDealt(10);
eventBus.InvokeEntityDied(enemyEntity);
```

### Thread-Safe Dispatch

```csharp
var threadSafeBus = new ThreadSafeEventBus();

// Safe to call from a background thread
threadSafeBus.InvokeDamageDealt(5);

// Call once per frame on the main thread
threadSafeBus.Flush();
```

---

## 🧬 Event API Source Generation

The [Event API Generator](CodeGen/EventExtensionsAPIAttribute.md) turns declarative `[EventExtensionsAPI]` classes into strongly-typed
extension methods for any [IEventBus](Bus/IEventBus.md) implementation. Declare event keys once and use generated
`Subscribe{Name}`, `Unsubscribe{Name}`, `Invoke{Name}`, `IsSubscribed{Name}`, and `Dispose{Name}` methods.

### Unsafe Mode

When the source generator emits unsafe calls, the generated methods use the runtime `SubscribeUnsafe`,
`UnsubscribeUnsafe`, and `InvokeUnsafe` APIs. These methods skip the safety checks performed by the regular
`Subscribe` / `Unsubscribe` / `Invoke` methods and are intended for performance-critical code paths where the event
key is known to be valid.

Enable unsafe generation for the whole class:

```csharp
[EventExtensionsAPI(Unsafe = true)]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> GameStarted = new(nameof(GameStarted));
}
```

Or opt-in per field:

```csharp
[EventExtensionsAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> GameStarted = new(nameof(GameStarted));

    [Unsafe]
    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));
}
```

> ⚠️ Unsafe event methods bypass runtime validation. Only use them when you have verified the subscription or event key
> state.

The underlying bus methods are documented on [IEventBus](Bus/IEventBus.md), [EventBus](Bus/EventBus.md), and
[Extensions](Extensions.md).

### Analyzer

The [Event API Analyzer](CodeGen/EventAPIAnalyzer.md) validates `[EventExtensionsAPI]` declarations and reports build
errors when event key fields are missing an initializer or are initialized with `new()` / `default`.

### Setup

The generator and analyzer DLLs are compile-time only. Add them to your Unity project as Roslyn analyzers:

1. Place the four DLLs in `Assets/Plugins/Atomic/SourceGenerators/`:
   - `EntityAPIGenerator.dll`
   - `EntityAPIAnalyzer.dll`
   - `EventAPIGenerator.dll`
   - `EventAPIAnalyzer.dll`
2. Select each DLL in the Unity Project window.
3. Add the asset label `RoslynAnalyzer`.
4. Under **Select platforms for plugin**, uncheck **Any Platform** and every individual platform.
5. Click **Apply** and restart Unity or run `Assets → Reimport All`.

For full build/deploy instructions, see the generator source in
`Assets/Plugins/Atomic/SourceGenerators/Project~`.

#### Inspecting generated source

The generators produce code **in-memory**. To write the generated files to disk, define the symbol:

```
ATOMIC_OUTPUT_SOURCEGEN_FILES
```

in `Edit → Project Settings → Player → Scripting Define Symbols`. Files are then written to:

```
Temp/GeneratedCode/
```

---

## 🔍 API Reference

### Bus Implementations

- [IEventBus](Bus/IEventBus.md) — core interface
- [EventBus](Bus/EventBus.md) — default implementation
- [ThreadSafeEventBus](Bus/ThreadSafeEventBus.md) — thread-safe wrapper with main-thread flushing
- [MonoEventBus](Bus/MonoEventBus.md) — Unity `MonoBehaviour` bus
- [MonoEventBusSingleton](Bus/MonoEventBusSingleton.md) — singleton scene/global bus
- [Bus Manual](Bus/Manual.md)

### Event Keys

- [EventKey](Keys/EventKey.md) — strongly-typed event identifier
- [EventKeyStore](Keys/EventKeyStore.md) — name-to-ID mapping
- [Keys Manual](Keys/Manual.md)

### Subscriptions

- [Subscription](Subscriptions/Subscription.md) — disposable subscription handle
- [Subscriptions Manual](Subscriptions/Manual.md)

### Extensions

- [EventBus Extensions](Extensions.md)

### Source Generation

- [Event API Generator](CodeGen/EventExtensionsAPIAttribute.md)
- [Event API Analyzer](CodeGen/EventAPIAnalyzer.md)
- [UnsafeAttribute](CodeGen/UnsafeAttribute.md)

---

## 📌 Best Practices

- Define event keys in a single `[EventExtensionsAPI]` class.
- Use generated extension methods for compile-time type safety.
- Dispose subscriptions to avoid leaks; prefer `using` declarations.
- Use `ThreadSafeEventBus` for background thread event dispatch.
- Call `Flush()` once per frame on the main thread for `ThreadSafeEventBus`.
- Keep event callbacks fast and side-effect free where possible.

