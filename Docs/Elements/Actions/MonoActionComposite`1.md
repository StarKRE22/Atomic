# 🧩 MonoActionComposite&lt;T&gt;

Represents a composite scene action with <b>one parameter</b> that can be invoked.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Inspector Settings](#-inspector-settings)
    - [Fields](#-fields)
        - [Actions](#actions)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)

---

## 🗂 Example of Usage

`MonoActionComposite<T>` can be used similarly to [MonoActionConfigurable&lt;T&gt;](MonoActionConfigurable%601.md) but is 
**strictly a composite container** for [MonoAction\<T>](MonoAction%601.md).

#### 1. Create a `GameObjectMonoActionComposite` component extending the base class.

```csharp
public sealed class GameObjectMonoActionComposite : MonoActionComposite<GameObject>
{
}
```

#### 2. Add the `GameObjectMonoActionComposite` component to a `GameObject`

<img src="../../Images/GameObjectSceneActionComposite.png" alt="MonoActionComposite example" width="" height="100">

#### 3. Create an action that destroys a `GameObject` (example)

```csharp
public sealed class DestroyGameObjectSceneAction : MonoAction<GameObject>
{
    public override void Invoke(GameObject arg) => Destroy(arg);
}
```

#### 4. Assign `DestroyGameObjectSceneAction` to the **Actions** parameter of the

`GameObjectMonoActionComposite` component

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class MonoActionComposite<T> : MonoAction<T>
```

- **Description:** Represents a composite scene action with <b>one parameter</b> that can be invoked.
- **Inheritance:** [MonoAction&lt;T&gt;](MonoAction%601.md)
- **Type parameter:** `T` — the argument type.
- **Notes:**
    - Supports Odin Inspector
    - Attach to a `GameObject`, assign a list of `MonoAction<T>` implementations in the Inspector, and they
      will be invoked sequentially.

---

### 🛠 Inspector Settings

| Parameter | Description                                                      |
|-----------|------------------------------------------------------------------|
| `actions` | The array of scene actions to invoke in order  with one argument |

---

### 🧱 Fields

#### `Actions`

```csharp
public MonoAction<T>[] actions;
```

- **Description:** The array of scene actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public override void Invoke(T arg);
```

- **Description:** Executes each action sequentially with the provided argument.
- **Parameter:** `arg` – The input argument.
