
---

<details>
  <summary>
    <h2>🧩 BaseEvent</h2>
    <br> Represents a <b>parameterless event</b> that can be subscribed to and invoked.
  </summary>

<br>

```csharp
public class BaseEvent : IEvent, IDisposable
```

---

### 🏹 Methods

#### `Subscribe(Action)`

```csharp
public Subscription Subscribe(Action action)  
```

- **Description:** Subscribes an action to be invoked whenever the event is triggered.
- **Parameter:** `action` – The delegate to be called when the value changes.
- **Returns:** A [Subscription](../Signals/Subscription.md#subscription) struct representing the active subscription.

#### `Unsubscribe(Action)`

```csharp
public void Unsubscribe(Action action)  
```

- **Description:** Removes a previously registered action so it will no longer be invoked when the event is triggered.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the event logic

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Clears all subscriptions for this event.

</details>

---

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

---

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

---

<details>
  <summary>
    <h2>🧩 BaseEvent&lt;T1, T2, T3&gt;</h2>
    <br> Represents an event that emits <b>three parameters</b>.
  </summary>

<br>

```csharp
public class BaseEvent<T1, T2, T3> : IEvent<T1, T2, T3>, IDisposable
```

- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
    - `T3` — The third argument

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2, T3>)`

```csharp
public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
```

- **Description:** Subscribes a handler to the event.
- **Parameter:** `action` – The delegate to invoke when the event triggers.
- **Returns:** A [Subscription<T1, T2, T3>](../Signals/Subscription.md#subscriptiont1-t2-t3) representing the active
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

</details>

---

<details>
  <summary>
    <h2>🧩 BaseEvent&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents an event that emits <b>four parameters</b>.
  </summary>

<br>

```csharp
public class BaseEvent<T1, T2, T3, T4> : IEvent<T1, T2, T3, T4>, IDisposable
```

- **Description:** Represents an event that emits **four parameters**.
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
    - `T3` — The third argument
    - `T4` — The fourth argument

---

### 🏹 Methods

#### `Subscribe(Action<T1, T2, T3, T4>)`

```csharp
public Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action)
```

- **Description:** Subscribes a handler to the event.
- **Parameter:** `action` – The delegate to invoke when the event triggers.
- **Returns:** A [Subscription<T1, T2, T3, T4>](../Signals/Subscription.md#subscriptiont1-t2-t3-t4) representing the
  active subscription.

#### `Unsubscribe(Action<T1, T2, T3, T4>)`

```csharp
public void Unsubscribe(Action<T1, T2, T3, T4> action)
```

- **Description:** Removes a previously registered handler from the event.
- **Parameters:** `action` – The delegate to remove from the subscription list.

#### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
```

- **Description:** Triggers the event with the specified arguments.
- **Parameters:**
    - `arg1` — The first argument
    - `arg2` — The second argument
    - `arg3` — The third argument
    - `arg4` — The fourth argument

#### `Dispose()`

```csharp
public void Dispose()
```

- **Description:** Clears all subscriptions for this event.

</details>

---
