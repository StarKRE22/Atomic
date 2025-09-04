# 🧩 EnableSubscription

A disposable subscription that detaches a callback from an `IEnableSource`'s **OnEnabled** event when disposed.  
Ensures **automatic unsubscription** and prevents memory leaks or unintended event calls.

---

## Overview
`EnableSubscription` provides a **disposable wrapper** for subscriptions to the `IEnableSource.OnEnabled` event.  
When disposed, it automatically unsubscribes the callback, ensuring **safe event handling**.

- Used by the framework for **temporary subscriptions** to enable events.
- Prevents **manual unsubscription mistakes**.
- Not intended for direct usage in user code.

---

## EnableSubscription
```csharp
public readonly struct EnableSubscription : IDisposable
```
---

## Members

### Constructor
```csharp
internal EnableSubscription(IEnableSource source, Action callback)
```
- Initializes a new subscription to the `OnEnabled` event of the given `IEnableSource`.
- **Parameters:**
    - `source` — The activatable source object to subscribe to.
    - `callback` — The callback to invoke when the source is enabled.

---

### Methods

#### Dispose
```csharp
public void Dispose();
```
- Unsubscribes the callback from the `IEnableSource.OnEnabled` event.
- Should be called when the subscription is no longer needed.
- Safe to call multiple times.

---

## Example Usage
```csharp
//Use extension method "WhenEnable()"
EnableSubscription handle = entity.WhenEnable(() => Console.WriteLine("Entity enabled!"));

//Unsubscribe later:
handle.Dispose();
```