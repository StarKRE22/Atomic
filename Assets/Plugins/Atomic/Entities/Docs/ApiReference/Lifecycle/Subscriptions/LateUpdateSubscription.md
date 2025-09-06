# 🧩 LateUpdateSubscription

A disposable subscription that detaches a callback from an `IUpdateSource`'s **OnLateUpdated** event when disposed.  
Ensures **automatic unsubscription** and prevents memory leaks or unintended event calls.

---

## Overview
`LateUpdateSubscription` provides a **disposable wrapper** for subscriptions to the `IUpdateSource.OnLateUpdated` event.  
When disposed, it automatically unsubscribes the callback, ensuring **safe event handling**.

- Used by the framework for **temporary or lifecycle-scoped subscriptions** to late update events.
- Prevents **manual unsubscription mistakes**.
- Not intended for direct usage in user code.

---

## LateUpdateSubscription
```csharp
public readonly struct LateUpdateSubscription : IDisposable
```
---

## Members

### Constructor
```csharp
internal LateUpdateSubscription(IUpdateSource source, Action<float> callback)
```
- Initializes a new subscription to the `OnLateUpdated` event of the given `IUpdateSource`.
- **Parameters:**
    - `source` — The updatable source object to subscribe to.
    - `callback` — The callback invoked during LateUpdate cycles.

---

### Methods

#### Dispose
```csharp
public void Dispose();
```
- Unsubscribes the callback from the `IUpdateSource.OnLateUpdated` event.
- Should be called when the subscription is no longer needed.
- Safe to call multiple times.

---

## Example Usage
```csharp
//Use extension method "WhenLateUpdate()"
LateUpdateSubscription handle = entity.WhenLateUpdate(delta => Console.WriteLine($"Entity late updated: {delta}"));

//Unsubcribe later
handle.Dispose();
```