# 🧩 IEvent&lt;T&gt;

```csharp
public interface IEvent<T> : ISignal<T>, IAction<T>
```
- **Description:** Represents an event that emits <b>one parameter</b>.
- **Type parameter:** `T` — The type of the event parameter.
- **Inheritance:** [ISignal&lt;T&gt;](ISignal%601.md), [IAction&lt;T&gt;](../Actions/IAction%601.md)

---

## 🏹 Methods

#### `Subscribe(Action<T>)`

```csharp
public Subscription<T> Subscribe(Action<T> action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [subscription](Subscription%601.md) struct representing the active
  subscription.

#### `Unsubscribe(Action<T>)`

```csharp
public void Unsubscribe(Action<T> action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameter:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Executes the event with the specified argument
- **Parameter:** `arg` — the input parameter
