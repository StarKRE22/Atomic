# 🧩 IAction&lt;T&gt;

Represents an executable action that <b>takes one argument</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)

---

## 🗂 Example of Usage

Implement `IAction<T>` for creating destroy action:

```csharp
public sealed class DestroyGameObjectAction : IAction<GameObject>
{
    public void Invoke(GameObject go) 
    {
        GameObject.Destroy(go);  
    } 
}
```

Use this action for destroying game objects from scene

```csharp
// Assume we have a GameObject
GameObject gameObject = ...

// Destroy object
IAction<GameObject> action = new DestroyGameObjectAction();
action.Invoke(gameObject);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IAction<in T>
```

- **Description:** Represents an executable action that <b>takes one argument</b>.
- **Type parameter:** `T` — the input parameter

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Executes the action with the specified argument
- **Parameter:** `arg` — the input parameter