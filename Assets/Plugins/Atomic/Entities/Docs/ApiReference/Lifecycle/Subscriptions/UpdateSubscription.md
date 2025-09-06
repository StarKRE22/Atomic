# 🧩 UpdateSubscription

A disposable subscription that detaches a callback from an `IUpdateSource`'s **OnUpdated** event when disposed.  
Ensures **automatic unsubscription** and prevents memory leaks or unintended event calls.

---

## Overview
`UpdateSubscription` provides a **disposable wrapper** for subscriptions to the `IUpdateSource.OnUpdated` event.  
When disposed, it automatically unsubscribes the callback, ensuring **safe event handling**.

- Used by the framework for **temporary subscriptions** to frame-based updates.
- Prevents **manual unsubscription mistakes**.
- Not intended for direct usage in user code.

---

## UpdateSubscription
```csharp
public readonly struct UpdateSubscription : IDisposable
```

---

## Members

### Constructor
```csharp
internal UpdateSubscription(IUpdateSource source, Action<float> callback)
```
- Initializes a new subscription to the `OnUpdated` event of the given `IUpdateSource`.
- **Parameters:**
    - `source` — The updatable source object to subscribe to.
    - `callback` — The callback to invoke on each update.

---

### Methods

#### Dispose
```csharp
public void Dispose();
```
- Unsubscribes the callback from the `IUpdateSource.OnUpdated` event.
- Should be called when the subscription is no longer needed.
- Safe to call multiple times.

---

## Example Usage
```csharp
//Use extension method "WhenUpdate()"
UpdateSubcription handle = entity.WhenUpdated(delta => Console.WriteLine($"Entity updated: {delta}"));

//Unsubscribe later:
handle.Dispose();
```