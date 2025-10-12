# 🧩 IEvent&lt;T1, T2, T3, T4&gt;

Represents an event that emits <b>four parameters</b>.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Events](#-events)
        - [OnEvent](#onevent)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3, T4)](#invoket1-t2-t3-t4)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IEvent<T1, T2, T3, T4> : 
    ISignal<T1, T2, T3, T4>, 
    IAction<T1, T2, T3, T4>
```

- **Description:** Represents an event that emits <b>four parameters</b>.
- **Inheritance:** [ISignal&lt;T1, T2, T3, T4&gt;](ISignal%604.md),
  [IAction&lt;T1, T2, T3, T4&gt;](../Actions/IAction%604.md)
- **Type parameters:**
    - `T1` — The first argument
    - `T2` — The second argument
    - `T3` — The third argument
    - `T4` — The fourth argument

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
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes the event with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument
    - `arg4` — the fourth argument