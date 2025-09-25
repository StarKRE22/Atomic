
<details>
  <summary>
    <h2>🧩 ISignal&lt;T&gt;</h2>
    <br> Represents a signal that notifies subscribers with a <b>single value</b>.
  </summary>

```csharp
public interface ISignal<T>
```

- **Type parameter:** `T` — the emitted value type.

---

### 🏹 Methods

#### `Subscribe(Action<T>)`

```csharp
public Subscription<T> Subscribe(Action<T> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](../Signals/Subscription.md#subscriptiont) that can be used to dispose of it.

#### `Unsubscribe(Action<T>)`

```csharp
public void Unsubscribe(Action<T> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>