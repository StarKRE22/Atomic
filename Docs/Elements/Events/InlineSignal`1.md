# 🧩 InlineSignal&lt;T&gt;

Represents a signal that notifies subscribers with a **single value**.

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
public class InlineSignal<T> : ISignal<T>
```

- **Description:** Represents a signal that notifies subscribers with a **single value**.
- **Type parameter:** `T` — the emitted value type.
- **Inheritance:** [ISignal&lt;T&gt;](ISignal%601.md)

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `Delegate Constructor`

```csharp
public InlineSignal(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
```

- **Description:** Initializes a new instance with provided delegates.
- **Parameters:**
    - `subscribe` — Action handling subscription logic
    - `unsubscribe` — Action handling unsubscription logic
- **Throws:** `ArgumentNullException` if `subscribe` or `unsubscribe` is null.

---

### ⚡ Events

#### `OnEvent`

```csharp
public event Action<T> OnEvent;
```

- **Description:** Occurs when the signal is emitted with single argument.
- **Parameters:** `T` — the emitted value.