# 🧩 DisableSubscription

A disposable subscription that detaches a callback from an `IEnableSource`'s **OnDisabled** event when disposed.  
Ensures **automatic unsubscription** and prevents memory leaks or unintended event calls.

---

## Overview
`DisableSubscription` provides a **disposable wrapper** for subscriptions to the `IEnableSource.OnDisabled` event.  
When disposed, it automatically unsubscribes the callback, ensuring **safe event handling**.

- Used by the framework for **temporary subscriptions** to disable events.
- Prevents **manual unsubscription mistakes**.
- Not intended for direct usage in user code.

---

## DisableSubscription
```csharp
public readonly struct DisableSubscription : IDisposable
```
---

## Members

### Constructor
```csharp
internal DisableSubscription(IEnableSource source, Action callback)
```
- Initializes a new subscription to the `OnDisabled` event of the given `IEnableSource`.
- **Parameters:**
    - `source` — The activatable source object to subscribe to.
    - `callback` — The callback to invoke when the source is disabled.
---

### Methods

#### Dispose
```csharp
public void Dispose();
```
- Unsubscribes the callback from the `IEnableSource.OnDisabled` event.
- Should be called when the subscription is no longer needed.
- Safe to call multiple times.

---

## Example Usage
```csharp
//Use extension method "WhenDisable()"
DisableSubscription handle = entity.WhenDisable(() => Console.WriteLine("Entity disabled!"));

//Unsubscribe later:
handle.Dispose();
```