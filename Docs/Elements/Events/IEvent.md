# 🧩 IEvent

```csharp
public interface IEvent : ISignal, IAction
```
- **Description:** Represents a <b>parameterless event</b> that can be subscribed to and invoked.
- **Inheritance:** [ISignal](ISignal.md), [IAction](../Actions/IAction.md)

---

## 🏹 Methods

#### `Subscribe(Action)`

```csharp
public Subscription Subscribe(Action action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [subscription](Subscription.md) struct representing the active subscription.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameter:** `action` – The delegate to remove from the subscription list.

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the event logic