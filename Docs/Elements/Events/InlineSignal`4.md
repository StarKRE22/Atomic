
<details>
  <summary>
    <h2>🧩 InlineSignal&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents a signal that notifies subscribers with <b>four values</b>.
  </summary>

```csharp
public sealed class InlineSignal<T1, T2, T3, T4> : ISignal<T1, T2, T3, T4>
```

- **Description:** Represents a signal that notifies subscribers with **four values**.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
    - `T4` — the fourth emitted value

---

### 🏗️ Constructors

#### `InlineSignal(Action<Action<T1, T2, T3, T4>> subscribe, Action<Action<T1, T2, T3, T4>> unsubscribe)`

```csharp
public InlineSignal(Action<Action<T1, T2, T3, T4>> subscribe, Action<Action<T1, T2, T3, T4>> unsubscribe)
```

- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2, T3, T4>)`

```csharp
public Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](../Signals/Subscription.md#subscriptiont1-t2-t3-t4) that can be used to dispose
  of it.

#### `Unsubscribe(Action<T1, T2, T3, T4>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3, T4> action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>