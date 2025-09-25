# 🧩 IEvent&lt;T1, T2&gt;

```csharp
public interface IEvent<T1, T2> : ISignal<T1, T2>, IAction<T1, T2>
```

- **Description:** Represents an event that emits <b>two parameters</b>.
- **Inheritance:** [ISignal&lt;T1, T2&gt;](ISignal%602.md), [IAction&lt;T1, T2&gt;](../Actions/IAction%602.md)
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument

---

## 🏹 Methods

#### `Subscribe(Action<T1, T2>)`

```csharp
public Subscription<T1, T2> Subscribe(Action<T1, T2> action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [subscription](Subscription%602.md) struct representing the active
  subscription.

#### `Unsubscribe(Action<T1, T2>)`

```csharp
public void Unsubscribe(Action<T1, T2> action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameter:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes the action with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument