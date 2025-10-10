# 🧩 IAction&lt;T1, T2, T3, T4&gt;

Represents an executable action that <b>takes four arguments</b>.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Methods](#-methods)
        - [Invoke(T1, T2, T3, T4)](#invoket1-t2-t3-t4)

---

## 🗂 Example of Usage

Below is an example of action that moves `Transform` by direction, speed and delta time:

```csharp
public sealed class MoveTransformAction : IAction<Transform, Vector3, float, float>
{
    public void Invoke(Transform transform, Vector3 direction, float speed, float deltaTime) 
    {
        transform.position += direction * (speed * deltaTime);
    }
}
```

Usage:

```csharp
IAction<Transform, Vector3, float, float> action = new MoveTransformAction();
action.Invoke(transform, Vector3.forward, 10, 0.02);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IAction<in T1, in T2, in T3, in T4>
```

- **Description:** Represents an executable action that <b>takes four arguments</b>.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

---

### 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes the action with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
    - `arg3` — the third argument
    - `arg4` — the fourth argument