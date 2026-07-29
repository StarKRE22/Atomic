# 🧩 MonoActionConfigurable&lt;T1, T2, T3&gt;

Represents a scene-based composite action with <b>three parameters</b>.
Attach to a `GameObject`, assign a list of [IAction\<T1, T2, T3>](IAction%603.md) implementations in the Unity Inspector, and they will
be invoked sequentially. Supports Odin Inspector.

---

## 📑 Table of Contents

- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Fields](#-fields)
        - [Actions](#actions)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3)](#invoket1-t2-t3)

---

## 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class MonoActionConfigurable<T1, T2, T3> : MonoAction<T1, T2, T3>
```

- **Description:** Represents a scene-based composite action with <b>three parameters</b>.
- **Inheritance:** [MonoAction&lt;T1, T2, T3&gt;](MonoAction%603.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument

---

### 🧱 Fields

#### `Actions`

```csharp
public IAction<T1, T2, T3>[] actions;
```

- **Description:** The array of actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public override void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes each action sequentially with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
