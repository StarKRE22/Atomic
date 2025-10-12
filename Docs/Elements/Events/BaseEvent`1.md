# 🧩 BaseEvent&lt;T&gt;

Represents an event that emits <b>one parameter</b>.

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class BaseEvent<T> : IEvent<T>, IDisposable
```
- **Description:** Represents an event that emits <b>one parameter</b>.
- **Inheritance:** [IEvent&lt;T&gt;](IEvent%601.md), `IDisposable`
- **Type parameter:** `T` — The type of the event argument.
- **Note:** Supports Unity serialization and Odin Inspector

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T> OnEvent;
```

- **Description:** Occurs when the signal is emitted with single argument.
- **Parameters:** `T` — the emitted value.

---

### 🏹 Methods

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

