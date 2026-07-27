# 🧩 MonoEventBus

**MonoEventBus** is a Unity `MonoBehaviour` implementation of [IEventBus](IEventBus.md). It can be attached to a
GameObject in the scene, making it easy to wire event handling to scene lifecycle.

---

## 📑 Table of Contents

- [Overview](#-overview)
- [Examples of Usage](#-examples-of-usage)
- [API Reference](#-api-reference)
- [Best Practices](#-best-practices)

---

## 🧩 Overview

`MonoEventBus` wraps a private [EventBus](EventBus.md) and exposes all `IEventBus` methods. It is automatically disposed
when the GameObject is destroyed.

Use it when you want an event bus tied to a specific scene or GameObject context.

---

## 🗂 Examples of Usage

### Add to Scene

1. Create an empty GameObject in the scene.
2. Add the **Atomic/Events/Event Bus** component.

### Use in Code

```csharp
public class GameContext : MonoEventBus
{
}

// Subscribe
MonoEventBus eventBus = ...;
using var subscription = eventBus.Subscribe(GameEventAPI.PlayerTurnStarted.Id, () =>
{
    Debug.Log("Player turn started");
});

// Invoke
eventBus.Invoke(GameEventAPI.PlayerTurnStarted.Id);
```

---

## 🔍 API Reference

### 🏛️ Type

```csharp
[AddComponentMenu("Atomic/Events/Event Bus")]
[DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
public partial class MonoEventBus : MonoBehaviour, IEventBus
```

- **Inheritance:** `MonoBehaviour`, [IEventBus](IEventBus.md)

### 🏹 Methods

Implements all methods from [IEventBus](IEventBus.md). The internal bus is disposed in `OnDestroy()`.

---

## 📌 Best Practices

- Place the event bus on a root GameObject in the scene.
- Use `MonoEventBusSingleton<E>` if you need a globally accessible bus instance.
- Dispose subscriptions manually when the listening object is destroyed.
- Keep callbacks lightweight to avoid frame spikes.
