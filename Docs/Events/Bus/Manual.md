# 📣 Event Bus

The **Atomic.Events** namespace contains event bus implementations used to publish and subscribe to events.
Buses decouple senders from receivers by routing events through integer keys, while strongly-typed `EventKey<TBus>`
structs and source-generated extension methods keep the API safe and discoverable.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
  - [Default EventBus](#default-eventbus)
  - [MonoEventBus Component](#monoeventbus-component)
  - [ThreadSafeEventBus](#threadsafeeventbus)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🗂 Examples of Usage

### Default EventBus

`EventBus` is the standard, single-threaded implementation. It is a good fit for pure C# logic and local context
entities.

```csharp
IEventBus bus = new EventBus();

using var subscription = bus.Subscribe(1, () => Debug.Log("Hello!"));
bus.Invoke(1); // Output: Hello!
```

With source-generated keys:

```csharp
using var subscription = bus.SubscribeDamageDealt(amount => Debug.Log($"Damage: {amount}"));
bus.InvokeDamageDealt(10);
```

### MonoEventBus Component

`MonoEventBus` is a `MonoBehaviour` bus that can be wired directly in the Unity Inspector. Add it via
`GameObject > Atomic > Events > Event Bus`.

```csharp
public class SampleUsage : MonoBehaviour
{
    [SerializeField] private MonoEventBus eventBus;

    private void Start()
    {
        eventBus.SubscribePlayerTurnStarted(() => Debug.Log("Player turn started"));
    }

    private void OnDestroy()
    {
        eventBus.Dispose();
    }
}
```

### ThreadSafeEventBus

`ThreadSafeEventBus` queues invokes from background threads and flushes them on the main thread.

```csharp
var bus = new ThreadSafeEventBus();

// Called from a job or background thread
bus.InvokeDamageDealt(5);

// Called from Unity's main Update loop
bus.Flush();
```

---

## 🔍 API Reference

- [IEventBus](IEventBus.md) — core event bus interface
- [EventBus](EventBus.md) — default implementation
- [ThreadSafeEventBus](ThreadSafeEventBus.md) — thread-safe wrapper that queues invokes for main-thread flushing
- [MonoEventBus](MonoEventBus.md) — Unity `MonoBehaviour` event bus
- [MonoEventBusSingleton](MonoEventBusSingleton.md) — singleton scene/global bus

---

## 📌 Best Practices

- Use `EventBus` for pure C# code and single-threaded logic.
- Use `MonoEventBus` for scene-local event wiring that designers can configure in the Inspector.
- Use `MonoEventBusSingleton<T>` for global buses or buses resolved by scene.
- Use `ThreadSafeEventBus` when invoking from background threads.
- Dispose subscriptions and the bus itself when the owner is destroyed to avoid leaks.
