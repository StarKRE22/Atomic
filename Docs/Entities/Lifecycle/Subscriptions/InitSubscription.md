# 🧩 InitSubscription

A disposable subscription that detaches a callback from an `IInitSource`'s **OnInitialized** event when disposed.  
Ensures **automatic unsubscription** and prevents memory leaks or unintended event calls.

---

## Overview
`InitSubscription` provides a **disposable wrapper** for subscriptions to the `IInitSource.OnInitialized` event.  
When disposed, it automatically unsubscribes the callback, ensuring **safe event handling**.

- Used by the framework for **temporary subscriptions** to initialization events.
- Prevents **manual unsubscription mistakes**.
- Not intended for direct usage in user code.

---

## InitSubscription
```csharp
public readonly struct InitSubscription : IDisposable
```

---

## Members

### Constructor
```csharp
internal InitSubscription(IInitSource source, Action callback)
- Initializes a new subscription to the `OnInitialized` event of the given `IInitSource`.
- **Parameters:**
    - `source` — The initialization source to subscribe to.
    - `callback` — The callback to invoke on initialization.
```

---

### Methods

#### Dispose
```csharp
public void Dispose();
```
- Unsubscribes the callback from the `IInitSource.OnInitialized` event.
- Should be called when the subscription is no longer needed.
- Safe to call multiple times.  


## Example Usage
```csharp
//Use extension method "WhenInit()"
InitSubscription handle = entity.WhenInit(() => Console.WriteLine("Entity initialized!"))
    
//Unsubscribe later:
handle.Dispose();
```