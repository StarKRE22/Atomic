# 🧩 ISignal&lt;T1, T2&gt;

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnEvent](#onevent)

---

```csharp
public interface ISignal<T1, T2>
```

- **Description:** Represents a signal that notifies subscribers with <b>two values</b>.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T1, T2> OnEvent;
```

- **Description:** Occurs when the signal is emitted with single argument.
- **Parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value