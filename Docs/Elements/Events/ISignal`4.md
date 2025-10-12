# 🧩 ISignal&lt;T1, T2, T3, T4&gt;

Represents a signal that notifies subscribers with **four values**.

---


## 📑 Table of Contents

- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Events](#-events)
    - [OnEvent](#onevent)
  
---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface ISignal<T1, T2, T3, T4>
```

- **Description:** Represents a signal that notifies subscribers with **four values**.
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value
    - `T4` — the fourth emitted value

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T1, T2, T3, T4> OnEvent;
```

- **Description:** Occurs when the signal is emitted with four arguments.
- **Parameters:**
  - `T1` — the first emitted value
  - `T2` — the second emitted value
  - `T3` — the third emitted value
  - `T4` — the fourth emitted value
  