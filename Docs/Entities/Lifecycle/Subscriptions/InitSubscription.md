# 🧩 InitSubscription

```csharp
public readonly struct InitSubscription : IDisposable
```

- **Description:** A disposable subscription that detaches a callback from an `IInitSource`'s **OnInitialized** event
  when disposed. Ensures **automatic unsubscription** and prevents memory leaks or unintended event calls.
- **Inheritance:** `IDisposable`
- **Note:**
    - Provides a safe way to subscribe temporarily to initialization events without manually unsubscribing.
    - Not intended for direct use in typical user code; mainly used by framework infrastructure.

---

## 🏗️ Constructor

```
internal InitSubscription(IInitSource source, Action callback)
```

- **Description:** Initializes a new subscription to the `OnInitialized` event of the given `IInitSource`.
- **Parameters:**
    - `source` — The initialization source to subscribe to.
    - `callback` — The callback to invoke when initialization occurs.

---

## 🏹 Methods

### `Dispose`

```
public void Dispose();
```

- **Description:** Unsubscribes the callback from the `IInitSource.OnInitialized` event.
- **Remarks:** Safe to call multiple times; should be called when the subscription is no longer needed.

---

## 🗂 Example of Usage

```
// Use extension method "WhenInit()"
InitSubscription handle = entity.WhenInit(() => Console.WriteLine("Entity initialized!"));

// Unsubscribe later:
handle.Dispose();
```
