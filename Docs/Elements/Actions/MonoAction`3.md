# 🧩 MonoAction&lt;T1, T2, T3&gt;

Represents a scene action with **three parameters**.

---

## 📑 Table of Contents

- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [Invoke(T1, T2, T3)](#invoket1-t2-t3)

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class MonoAction<T1, T2, T3> : MonoBehaviour, IAction<T1, T2, T3>
```
- **Description:** Represents a scene action with **three parameters**.
- **Inheritance:** `MonoBehaviour`, [IAction&lt;T1, T2, T3&gt;](IAction%603.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
- **Note:** Attach to a GameObject and implement `Invoke(T1, T2, T3)` to define custom behavior.

---

### 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public abstract void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes the action logic with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
