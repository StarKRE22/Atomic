# 🧩 DisposeSubscription

```csharp
public readonly struct DisposeSubscription : IDisposable
```

- **Description:** Represents a disposable subscription handle for an [IInitLifecycle's](../Sources/IInitLifecycle.md) 
  **OnDisposed** event. Automatically unsubscribes the callback when disposed, ensuring safe cleanup and preventing memory leaks.
- **Inheritance:** `IDisposable`

---

## 🏗️ Constructor

```csharp
public DisposeSubscription(IInitLifecycle source, Action callback)
```

- **Description:** Subscribes the provided callback to the `OnDisposed` event of the given source.
- **Parameters:**
    - `source` — The initialization source to subscribe to.
    - `callback` — The callback action invoked upon disposal.

---

## 🏹 Methods

### `Dispose`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnDisposed` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.
