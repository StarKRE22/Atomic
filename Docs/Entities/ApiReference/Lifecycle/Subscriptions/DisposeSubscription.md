# 🧩 DisposeSubscription

A disposable subscription that detaches a callback from an `IInitSource`'s **OnDisposed** event when disposed.  
Ensures **automatic unsubscription** and prevents memory leaks or unintended event calls.

---

## Overview
`DisposeSubscription` provides a **disposable wrapper** for subscriptions to the `IInitSource.OnDisposed` event.  
When disposed, it automatically unsubscribes the callback, ensuring **safe event handling**.

- Used by the framework for **temporary or one-time subscriptions** to despawn events.
- Prevents **manual unsubscription mistakes**.
- Not intended for direct usage in user code.

---

## DisposeSubscription
```csharp
public readonly struct DisposeSubscription : IDisposable
```
---

## Members

### Constructor
!!!
internal DisposeSubscription(IInitSource source, Action callback)
!!!
- Initializes a new subscription to the `OnDisposed` event of the given `IInitSource`.
- **Parameters:**
    - `source` — The spawnable entity the callback is subscribed to.
    - `callback` — The callback to invoke on despawn.

---

### Methods

#### Dispose
```csharp
public void Dispose();
```
- Unsubscribes the callback from the `IInitSource.OnDisposed` event.
- Should be called when the subscription is no longer needed.
- Safe to call multiple times.

---

## Example Usage
```csharp
//Use extension method "WhenDispose()"
DisposeSubscription handle = entity.WhenDisposed(() => Console.WriteLine("Entity disposed!"));

//Unsubscribe later:
handle.Dispose();
```
