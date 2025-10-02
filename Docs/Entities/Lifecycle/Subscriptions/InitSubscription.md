# 🧩 InitSubscription

```csharp
public readonly struct InitSubscription : IDisposable
```

- **Description:** Represents a disposable subscription handle for an [IInitLifecycle's](../Sources/IInitLifecycle.md) **OnInitialized** event.  
  Automatically unsubscribes the callback when disposed, preventing memory leaks and repeated invocations.
- **Inheritance:** `IDisposable`

---

## 🏗️ Constructor

```csharp
public InitSubscription(IInitLifecycle source, Action callback)
```

- **Description:** Subscribes the provided callback to the `OnInitialized` event of the given source.
- **Parameters:**
  - `source` — The initialization source to subscribe to.
  - `callback` — The callback action invoked upon initialization.

---

## 🏹 Methods

### `Dispose`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnInitialized` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.
