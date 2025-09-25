
<details>
  <summary>
    <h2>🧩 IEvent&lt;T1&gt;</h2>
    <br> Represents an event that emits <b>one parameter</b>.
  </summary>

<br>

```csharp
public interface IEvent<T> : ISignal<T>, IAction<T>
```

- **Type parameter:** `T` — The type of the event parameter.

---

### 🏹 Methods

#### `Subscribe(Action<T>)`

```csharp
public Subscription<T> Subscribe(Action<T> action)  
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription&lt;T&gt;](../Signals/Subscription.md#subscriptiont) struct representing the active
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

</details>