
<details>
  <summary>
    <h2>🧩 ISignal&lt;T1, T2&gt;</h2>
    <br> Represents a signal that notifies subscribers with <b>two values</b>.
  </summary>

```csharp
public interface ISignal<T1, T2>
```

- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2>)`

```csharp
public Subscription<T1, T2> Subscribe(Action<T1, T2> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:**  The active [subscription](../Signals/Subscription.md#subscriptiont1-t2) that can be used to dispose of
  it.

#### `Unsubscribe(Action<T1, T2>)`

```csharp
public void Unsubscribe(Action<T1, T2> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>