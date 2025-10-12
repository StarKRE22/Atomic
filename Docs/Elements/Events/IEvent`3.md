# 🧩 IEvent&lt;T1, T2, T3&gt;

Represents an event that emits <b>three parameters</b>.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnEvent](#onevent)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3)](#invoket1-t2-t3)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEvent<T1, T2, T3> : ISignal<T1, T2, T3>, IAction<T1, T2, T3>
```

- **Description:** Represents an event that emits <b>three parameters</b>.
- **Inheritance:** [ISignal&lt;T1, T2, T3&gt;](ISignal%603.md), [IAction&lt;T1, T2, T3&gt;](../Actions/IAction%603.md)
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
    - `T3` — The third argument

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

---

### 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes the event with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument