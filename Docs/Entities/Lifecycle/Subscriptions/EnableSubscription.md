# 🧩 EnableSubscription

```csharp
public readonly struct EnableSubscription : IDisposable
```

- **Description:** Represents a disposable subscription handle for
  an [IEnableLifecycle's](../Sources/IEnableLifecycle.md) **OnEnabled** event. Automatically unsubscribes the callback
  when disposed, ensuring safe event management and preventing repeated invocations.
- **Inheritance:** `IDisposable`

---

## 🏗️ Constructor

```csharp
public EnableSubscription(IEnableLifecycle source, Action callback)
```

- **Description:** Subscribes the provided callback to the `OnEnabled` event of the given source.
- **Parameters:**
    - `source` — The activatable source to subscribe to.
    - `callback` — The callback action invoked when the source is enabled.

---

## 🏹 Methods

#### `Dispose`

```csharp
public void Dispose();
```

- **Description:** Detaches the callback from the `OnEnabled` event.
- **Remarks:** Safe to call multiple times; should be invoked when the subscription is no longer needed.