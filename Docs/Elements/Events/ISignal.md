

---

<details>
  <summary>
    <h2>🧩 ISignal</h2>
    <br> Represents a signal that can notify subscribers of events <b>without passing any data</b>.
  </summary>

<br>

```csharp
public interface ISignal
```

---

### 🏹 Methods

#### `Subscribe(Action)`

```csharp
public Subscription Subscribe(Action action)
```

- **Description:** Subscribes an action to be invoked whenever the signal is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** The active [subscription](../Signals/Subscription.md#subscription) that can be used to dispose of it.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the signal is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

</details>

---


---


---


---

---
