# 🧩 SceneActionDefault&lt;T1, T2, T3, T4&gt;

Represents a scene-based composite action with <b>four parameters</b>.
Attach to a `GameObject`, assign a list of [IAction\<T1, T2, T3, T4>](IAction%604.md) implementations in the
Unity Inspector, and they will be invoked sequentially. Supports Odin Inspector.

---

## 📑 Table of Contents

- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Fields](#-fields)
        - [Actions](#actions)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3, T4)](#invoket1-t2-t3-t4)

---

## 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public abstract class SceneActionDefault<T1, T2, T3, T4> : SceneActionAbstract<T1, T2, T3, T4>
```

- **Description:** Represents a scene-based composite action with <b>four parameters</b>.
- **Inheritance:** [SceneActionAbstract&lt;T1, T2, T3, T4&gt;](SceneActionAbstract%604.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

---

### 🧱 Fields

#### `Actions`

```csharp
public IAction<T1, T2, T3, T4>[] actions;
```

- **Description:** The array of actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public override void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes each action sequentially with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument
    - `arg4` – The fourth argument