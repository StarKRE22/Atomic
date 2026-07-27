# 📣 Event API Generator

The **Event API Generator** is a Roslyn incremental source generator that reads a static class marked with `[EventAPI]`
and emits strongly-typed event-bus extension methods. It eliminates magic event ids and gives you autocomplete for
subscribing, invoking, and unsubscribing from events.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Requirements](#-requirements)
- [Setup](#-setup)
- [Declaring an API Class](#-declaring-an-api-class)
- [Supported Key Shapes](#-supported-key-shapes)
- [Generated Methods](#-generated-methods)
- [Analyzer](#-analyzer)
- [Examples of Usage](#-examples-of-usage)
- [Troubleshooting](#-troubleshooting)

---

## 🧩 Overview

Write event keys as static fields:

```csharp
using Atomic.Events;

[EventAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus, int> DamageDealt = new(nameof(DamageDealt));
    public static readonly EventKey<IEventBus, IGameEntity, int> EntityHealed = new(nameof(EntityHealed));
}
```

After the first compilation, the following methods become available automatically:

```csharp
IEventBus bus = new EventBus();

bus.InvokePlayerTurnStarted();
bus.InvokeDamageDealt(10);
bus.InvokeEntityHealed(entity, 25);

using var subscription = bus.SubscribePlayerTurnStarted(() => Debug.Log("Started"));
```

---

## 📝 Requirements

- Unity 6 (6000.0 LTS or newer)
- The project must reference the **Atomic.Events** runtime assembly
- The `EventAPIGenerator.dll` must be loaded as a Roslyn analyzer

See [Setup.md](../Setup.md) for the shared setup instructions.

---

## ⚙️ Setup

See [Setup.md](../Setup.md) for how to add the generator DLL to your Unity project.

The generator is deployed at:

```
Assets/Plugins/Atomic/SourceGenerators/EventAPIGenerator.dll
```

---

## 🗂 Declaring an API Class

An API class must be:

- `public static partial class`
- Decorated with `[EventAPI]`
- Contain static fields of type `EventKey<>` from the `Atomic.Events` namespace

```csharp
[EventAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus, IGameEntity> EntityDied = new(nameof(EntityDied));
}
```

Every key field must be initialized with a non-default constructor. The analyzer reports `new()` or `default` as an error.

---

## 🔍 Supported Key Shapes

The generator supports `EventKey<>` with one to four generic arguments:

| Key type | Arguments | Generated `Invoke` signature |
|----------|-----------|------------------------------|
| `EventKey<TBus>` | 0 | `Invoke{Name}(this TBus bus)` |
| `EventKey<TBus, T>` | 1 | `Invoke{Name}(this TBus bus, T arg)` |
| `EventKey<TBus, T1, T2>` | 2 | `Invoke{Name}(this TBus bus, T1 arg1, T2 arg2)` |
| `EventKey<TBus, T1, T2, T3>` | 3 | `Invoke{Name}(this TBus bus, T1 arg1, T2 arg2, T3 arg3)` |

The bus type is taken from the **first generic argument**, so the same class can target different bus types if needed.

---

## 🔍 Generated Methods

For each event key the generator creates:

| Method | Description |
|--------|-------------|
| `Subscription Subscribe{Name}(this TBus bus, Action action)` | Subscribe a callback with matching arity. |
| `void Unsubscribe{Name}(this TBus bus, Action action)` | Unsubscribe a callback. |
| `void Invoke{Name}(this TBus bus, ...)` | Invoke the event. |
| `bool IsSubscribed{Name}(this TBus bus)` | Check if any callback is registered. |
| `bool Dispose{Name}(this TBus bus)` | Dispose all subscriptions for the event. |

For parameterized events, `Action` and `Subscription` are generic (`Action<T>`, `Subscription<T>`, etc.).

---

## 🔬 Analyzer

Deploy the [Event API Analyzer](EventAPIAnalyzer.md) alongside the generator. It reports two errors:

| Rule | Description |
|------|-------------|
| `EAPI0001` | Event key field has no initializer. |
| `EAPI0002` | Event key field is initialized with `new()` or `default`. |

Both diagnostics come with a code fix that inserts `= new(nameof(FieldName))`.

---

## 🗂 Examples of Usage

### Definition

```csharp
using Atomic.Events;

[EventAPI]
public static partial class GameEventAPI
{
    public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
    public static readonly EventKey<IEventBus, IGameEntity> EntityDied = new(nameof(EntityDied));
    public static readonly EventKey<IEventBus, IGameEntity, int> EntityHealed = new(nameof(EntityHealed));
}
```

### Subscription

```csharp
public sealed class TurnPresenter
{
    private readonly IEventBus _eventBus;
    private readonly List<IDisposable> _subscriptions = new();

    public TurnPresenter(IEventBus eventBus)
    {
        _eventBus = eventBus;
        _subscriptions.Add(_eventBus.SubscribePlayerTurnStarted(OnTurnStarted));
        _subscriptions.Add(_eventBus.SubscribeEntityDied(OnEntityDied));
    }

    private void OnTurnStarted()
    {
        Debug.Log("Player turn started");
    }

    private void OnEntityDied(IGameEntity entity)
    {
        Debug.Log($"Entity died: {entity}");
    }
}
```

### Invocation

```csharp
public sealed class TurnUseCase
{
    private readonly IEventBus _eventBus;

    public TurnUseCase(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public void StartPlayerTurn()
    {
        _eventBus.InvokePlayerTurnStarted();
    }

    public void KillEntity(IGameEntity entity)
    {
        _eventBus.InvokeEntityDied(entity);
    }
}
```

---

## 🔧 Troubleshooting

### Generated methods do not appear

1. Confirm `EventAPIGenerator.dll` is in `Assets/Plugins/Atomic/SourceGenerators/`.
2. Confirm the DLL has the `RoslynAnalyzer` asset label.
3. Confirm all platforms are unchecked in the DLL import settings.
4. Restart Unity or run `Assets → Reimport All`.

### Build errors about missing initializers

Every field must be initialized with a non-default constructor, e.g.:

```csharp
public static readonly EventKey<IEventBus> PlayerTurnStarted = new(nameof(PlayerTurnStarted));
```

`new()` and `default` are reported as `EAPI0002`.

### Generated file is not written to disk

The generator works in-memory. To dump generated files, add the scripting define symbol:

```
ATOMIC_OUTPUT_SOURCEGEN_FILES
```

and look in `Temp/GeneratedCode/`.

---

## 📦 Source Repository

The generator source code is available at:

**https://github.com/dre0dru/Atomic.SourceGenerators**
