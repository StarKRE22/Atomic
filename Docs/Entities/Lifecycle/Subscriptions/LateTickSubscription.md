# 🧩 LateTickSubscription

```csharp
public readonly struct LateTickSubscription : IDisposable
```

- **Description:** Represents a disposable subscription handle for an [ITickLifecycle's](../Sources/ITickLifecycle.md)
  **OnLateTicked** event. Automatically unsubscribes the callback when disposed, ensuring safe handling of late update
  logic.
- **Inheritance:** `IDisposable`

---

## 🏗️ Constructor

```csharp
public LateTickSubscription(ITickLifecycle source, Action<float> callback)
```

- **Description:** Subscribes the provided callback to the `OnLateTicked` event of the given source.
- **Parameters:**
    - `source` — The updatable source to subscribe to.
    - `callback` — The callback action invoked during the LateUpdate phase.

---

## 🏹 Methods

#### `Dispose`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnLateTicked` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.