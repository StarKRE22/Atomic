# 🧩 FixedUpdateSubscription

A disposable subscription that detaches a callback from an `IUpdateSource`'s **OnFixedUpdated** event when disposed.  
Ensures **automatic unsubscription** and prevents memory leaks or unintended event calls.

---

## Overview
`FixedUpdateSubscription` provides a **disposable wrapper** for subscriptions to the `IUpdateSource.OnFixedUpdated` event.  
When disposed, it automatically unsubscribes the callback, ensuring **safe event handling**.

- Used by the framework for **temporary subscriptions** to fixed update events (typically physics updates).
- Prevents **manual unsubscription mistakes**.
- Not intended for direct usage in user code.

---

## FixedUpdateSubscription
```csharp
public readonly struct FixedUpdateSubscription : IDisposable
```

---

## Members

### Constructor
```csharp
internal FixedUpdateSubscription(IUpdateSource source, Action<float> callback)
```
- Initializes a new subscription to the `OnFixedUpdated` event of the given `IUpdateSource`.
- **Parameters:**
    - `source` — The updatable source object to subscribe to.
    - `callback` — The callback to invoke on each fixed update.

---

### Methods

#### Dispose
```csharp
public void Dispose();
```
- Unsubscribes the callback from the `IUpdateSource.OnFixedUpdated` event.
- Should be called when the subscription is no longer needed.
- Safe to call multiple times.

---

## Example Usage
```csharp
//Use extension method "WhenFixedUpdate()"
FixedUpdateSubscription handle = entity.WhenFixedUpdated(delta => Console.WriteLine($"Entity fixed updated: {delta}"));

//Unsubcribe later
handle.Dispose();
```