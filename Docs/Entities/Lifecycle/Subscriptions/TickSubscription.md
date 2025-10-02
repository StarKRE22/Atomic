# 🧩 TickSubscription

```csharp
public readonly struct TickSubscription : IDisposable
```

- **Description:** Represents a disposable subscription handle for an [ITickLifecycle's](../Sources/ITickLifecycle.md)
  **OnTicked** event. Automatically unsubscribes the callback when disposed, preventing memory leaks and repeated frame
  updates.
- **Inheritance:** `IDisposable`

---

## 🏗️ Constructor

```csharp
public TickSubscription(ITickLifecycle source, Action<float> callback)
```

- **Description:** Subscribes the provided callback to the `OnTicked` event of the given source.
- **Parameters:**
    - `source` — The updatable source to subscribe to.
    - `callback` — The callback action invoked every frame during the Update phase.

---

## 🏹 Methods

#### `Dispose`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnTicked` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.