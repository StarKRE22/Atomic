# 🧩 BaseEvent&lt;T1, T2&gt;

Represents an event that emits <b>two parameters</b>.

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class BaseEvent<T1, T2> : IEvent<T1, T2>, IDisposable
```

- **Description:** Represents an event that emits <b>two parameters</b>.
- **Inheritance:** [IEvent&lt;T1, T2&gt;](IEvent%602.md), `IDisposable`
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
- **Note:** Supports Unity serialization and Odin Inspector

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T1, T2> OnEvent;
```

- **Description:** Occurs when the signal is emitted with two arguments.
- **Parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value

---

### 🏹 Methods

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