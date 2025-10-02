# 🧩 FixedTickSubscription

```csharp
public readonly struct FixedTickSubscription : IDisposable
```

- **Description:** Represents a disposable subscription handle for an [ITickLifecycle's](../Sources/ITickLifecycle.md)
  **OnFixedTicked** event. Automatically unsubscribes the callback when disposed, ensuring safe handling of physics or
  fixed-timestep updates.
- **Inheritance:** `IDisposable`

---

## 🏗️ Constructor

```csharp
public FixedTickSubscription(ITickLifecycle source, Action<float> callback)
```

- **Description:** Subscribes the provided callback to the `OnFixedTicked` event of the given source.
- **Parameters:**
    - `source` — The updatable source to subscribe to.
    - `callback` — The callback action invoked during the FixedUpdate phase.

---

## 🏹 Methods

#### `Dispose`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnFixedTicked` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.