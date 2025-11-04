# 🧩 Event&lt;T1, T2, T3, T4&gt;

Represents an event that emits **four parameters**.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnEvent](#onevent)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3, T4)](#invoket1-t2-t3-t4)
        - [Dispose()](#dispose)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class Event<T1, T2, T3, T4> : IEvent<T1, T2, T3, T4>, IDisposable
```

- **Description:** Represents an event that emits **four parameters**.
- **Inheritance:** [IEvent&lt;T1, T2, T3, T4&gt;](IEvent%604.md), `IDisposable`
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
    - `T3` — The third argument
    - `T4` — The fourth argument
- **Note:** Supports Unity serialization and Odin Inspector

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

---

### 🏹 Methods

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