
<details>
  <summary>
    <h2>🧩 BaseEvent&lt;T&gt;</h2>
    <br> Represents an event that emits <b>one parameter</b>.
  </summary>

<br>

```csharp
public class BaseEvent<T> : IEvent<T>, IDisposable
```

- **Type parameter:** `T` — The type of the event argument.

---

### 🏹 Methods

#### `Subscribe(Action<T>)`

```csharp
public Subscription<T> Subscribe(Action<T> action)
```

- **Description:** Subscribes a handler to the event.
- **Parameter:** `action` – The delegate to invoke when the event triggers.
- **Returns:** A [Subscription<T>](../Signals/Subscription.md#subscriptiont) representing the active subscription.

#### `Unsubscribe(Action<T>)`

```csharp
public void Unsubscribe(Action<T> action)
```

- **Description:** Removes a previously registered handler from the event.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Triggers the event with the specified argument.
- **Parameter:** `arg` — The input parameter.

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Clears all subscriptions for this event.

</details>
