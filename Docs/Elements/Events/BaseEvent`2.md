
<details>
  <summary>
    <h2>🧩 BaseEvent&lt;T1, T2&gt;</h2>
    <br> Represents an event that emits <b>two parameters</b>.
  </summary>

<br>

```csharp
public class BaseEvent<T1, T2> : IEvent<T1, T2>, IDisposable
```

- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2>)`

```csharp
public Subscription<T1, T2> Subscribe(Action<T1, T2> action)
```

- **Description:** Subscribes a handler to the event.
- **Parameter:** `action` – The delegate to invoke when the event triggers.
- **Returns:** A [Subscription<T1, T2>](../Signals/Subscription.md#subscriptiont1-t2) representing the active
  subscription.

#### `Unsubscribe(Action<T1, T2>)`

```csharp
public void Unsubscribe(Action<T1, T2> action)
```

- **Description:** Removes a previously registered handler from the event.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2)
```

- **Description:** Triggers the event with the specified arguments.
- **Parameters:**
    - `arg1` — The first argument
    - `arg2` — The second argument

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Clears all subscriptions for this event.

</details>