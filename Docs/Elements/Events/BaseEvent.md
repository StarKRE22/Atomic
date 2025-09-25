# 🧩 BaseEvent

```csharp
public class BaseEvent : IEvent, IDisposable
```

- **Description:** Represents a <b>parameterless event</b> that can be subscribed to and invoked.
- **Inheritance:** [IEvent](IEvent.md), `IDisposable`

---

## 🏹 Methods

#### `Subscribe(Action)`

```csharp
public Subscription Subscribe(Action action)  
```

- **Description:** Subscribes an action to be invoked whenever the event is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [subscription](Subscription.md) struct representing the active subscription.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the event is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the event logic

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Clears all subscriptions for this event.
