# 🧩 BaseEvent&lt;T1, T2, T3&gt;

```csharp
[Serializable]
public class BaseEvent<T1, T2, T3> : IEvent<T1, T2, T3>, IDisposable
```

- **Description:**  Represents an event that emits <b>three parameters</b>.
- **Inheritance:** [IEvent&lt;T1, T2, T3&gt;](IEvent%603.md), `IDisposable`
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
    - `T3` — The third argument
- **Note:** Supports Unity serialization and Odin Inspector

---

## 🏹 Methods

#### `Subscribe(Action<T1, T2, T3>)`

```csharp
public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
```

- **Description:** Subscribes a handler to the event.
- **Parameter:** `action` – The delegate to invoke when the event triggers.
- **Returns:** A [subscription](Subscription%603.md) representing the active
  subscription.

#### `Unsubscribe(Action<T1, T2, T3>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3> action)
```

- **Description:** Removes a previously registered handler from the event.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3)
```

- **Description:** Triggers the event with the specified arguments.
- **Parameters:**
    - `arg1` — The first argument
    - `arg2` — The second argument
    - `arg3` — The third argument

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Clears all subscriptions for this event.