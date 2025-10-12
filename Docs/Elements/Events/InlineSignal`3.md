# 🧩 InlineSignal&lt;T1, T2, T3&gt;

Represents a signal that notifies subscribers with **three values**.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [Delegate Constructor](#delegate-constructor)
    - [Events](#-events)
        - [OnEvent](#onevent)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineSignal<T1, T2, T3> : ISignal<T1, T2, T3>
```

- **Description:** Represents a signal that notifies subscribers with **three values**.
- **Inheritance:** [ISignal&lt;T1, T2, T3&gt;](ISignal%603.md)
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `Delegate Constructor`

```csharp
public InlineSignal(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
```

- **Description:** Initializes a new instance with provided subscription and unsubscription delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T1, T2, T3> OnEvent;
```

- **Description:** Occurs when the signal is emitted with three arguments.
- **Parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value
    - `T3` — the third emitted value