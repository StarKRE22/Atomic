# 🧩 IEvent&lt;T1, T2, T3&gt;

```csharp
public interface IEvent<T1, T2, T3> : ISignal<T1, T2, T3>, IAction<T1, T2, T3>
```
- **Description:** Represents an event that emits <b>three parameters</b>.
- **Inheritance:** [ISignal&lt;T1, T2, T3&gt;](ISignal%603.md), [IAction&lt;T1, T2, T3&gt;](../Actions/IAction%603.md)
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
    - `T3` — The third argument

---

## 🏹 Methods

#### `Subscribe(Action<T1, T2, T3>)`

```csharp
public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [subscription](Subscription%603.md) struct representing the
  active subscription.

#### Unsubscribe(Action<T1, T2, T3>)

```csharp
public void Unsubscribe(Action<T1, T2, T3> action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameter:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes the event with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument