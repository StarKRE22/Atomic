# 🧩 InlineSignal&lt;T1, T2&gt;

Represents a reactive signal with **two parameters**.

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
public class InlineSignal<T1, T2> : ISignal<T1, T2>
```

- **Description:** Represents a reactive signal with **two parameters**.
- **Inheritance:** [ISignal&lt;T1, T2&gt;](ISignal%602.md)
- **Type parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `Delegate Constructor`

```csharp
public InlineSignal(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
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
public event Action<T1, T2> OnEvent;
```

- **Description:** Occurs when the signal is emitted with two arguments.
- **Parameters:**
    - `T1` — the first emitted value
    - `T2` — the second emitted value