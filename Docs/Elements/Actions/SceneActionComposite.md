# 🧩 SceneActionComposite Classes

The `SceneActionComposite` class represents a **group of [SceneActionAbstract](SceneActionAbstract.md) instances** that can be invoked sequentially.  
It follows the [Composite design pattern]((https://en.wikipedia.org/wiki/Composite_pattern)): the group itself behaves as a single scene action, while internally invoking all contained scene actions in order.

This class is ideal for **building complex scene behaviors** directly in the Unity Inspector without writing extra code.

> [!NOTE]  
> Actions are executed in the order they appear in the array.  
> Null references are automatically skipped, making partially configured lists safe to use.

---

## 🧩 SceneActionComposite
```csharp
public class SceneActionComposite : SceneActionAbstract
```
- **Description:** Represents a **parameterless composite scene action**.
- **Usage:** Attach to a `GameObject`, assign a list of `SceneActionAbstract` implementations in the Inspector, and they will be invoked sequentially.

### Inspector Settings

| Parameter | Type                     | Description                             |
|-----------|-------------------------|-----------------------------------------|
| `actions` | `SceneActionAbstract[]` | The array of scene actions to invoke in order |

### Methods

#### `Invoke()`
```csharp
public override void Invoke();
```
- **Description:** Executes each action in the `actions` array sequentially.

---

## 🧩 SceneActionComposite&lt;T&gt;
```csharp
public class SceneActionComposite<T> : SceneActionAbstract<T>
```
- **Description:** Composite scene action with **one parameter**.
- **Type parameter:** `T` — the argument type.

### Inspector Settings

| Parameter | Type                       | Description                             |
|-----------|----------------------------|-----------------------------------------|
| `actions` | `SceneActionAbstract<T>[]` | The array of actions to execute sequentially with an argument |

### Methods
#### `Invoke(T arg)`
```csharp
public override void Invoke(T arg);
```
- **Description:** Executes each action sequentially with the provided argument.

---

## 🧩 SceneActionComposite&lt;T1, T2&gt;
```csharp
public class SceneActionComposite<T1, T2> : SceneActionAbstract<T1, T2>
```
- **Description:** Composite scene action with **two parameters**.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument

### Inspector Settings

| Parameter | Type                          | Description                             |
|-----------|-------------------------------|-----------------------------------------|
| `actions` | `SceneActionAbstract<T1, T2>[]` | The array of actions to execute sequentially with two arguments |

### Methods

#### `Invoke(T1 arg1, T2 arg2)`
```csharp
public override void Invoke(T1 arg1, T2 arg2);
```
- **Description:** Executes each action sequentially with the provided arguments.

---

## 🧩 SceneActionComposite&lt;T1, T2, T3&gt;
```csharp
public class SceneActionComposite<T1, T2, T3> : SceneActionAbstract<T1, T2, T3>
```
- **Description:** Composite scene action with **three parameters**.
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument

### Inspector Settings

| Parameter | Type                               | Description                             |
|-----------|------------------------------------|-----------------------------------------|
| `actions` | `SceneActionAbstract<T1, T2, T3>[]` | The array of actions to execute sequentially with three arguments |

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`
```csharp
public override void Invoke(T1 arg1, T2 arg2, T3 arg3);
```
- **Description:** Executes each action sequentially with the provided arguments.

---

## 🧩 SceneActionComposite&lt;T1, T2, T3, T4&gt;
```csharp
public class SceneActionComposite<T1, T2, T3, T4> : SceneActionAbstract<T1, T2, T3, T4>
```
- **Description:** Composite scene action with **four parameters**.
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument
    - `T4` — fourth argument

### Inspector Settings

| Parameter | Type                                   | Description                             |
|-----------|----------------------------------------|-----------------------------------------|
| `actions` | `SceneActionAbstract<T1, T2, T3, T4>[]` | The array of actions to execute sequentially with four arguments |

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)`
```csharp
public override void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```
- **Description:** Executes each action sequentially with the provided arguments.

---

## 🗂 Example of Usage

`SceneActionComposite` can be used similarly to [SceneActionDefault](SceneActionDefault.md) but is **strictly a composite container for `SceneActionAbstract`**.  

### 🔹 Non-generic Usage

#### 1. Add the `Atomic/Elements/Action Composite` component to a `GameObject`.
  
<img src="../../Images/SceneActionComposite.png" alt="SceneActionComposite example" width="" height="100">

#### 2. Assign `HelloWorldSceneAction` component to the **Actions** array in the Inspector.

```csharp
public sealed class HelloWorldSceneAction : SceneActionAbstract
{
    public override void Invoke() => Debug.Log("Hello world");
}
```

### 🔹 Generic Usage

#### 1. Create a `GameObjectSceneActionComposite` component.
```csharp
using Atomic.Elements;
using UnityEngine;

public sealed class GameObjectSceneActionComposite : SceneActionComposite<GameObject>
{
}
```
#### 2. Add the `GameObjectSceneActionComposite` component to a `GameObject`

<img src="../../Images/GameObjectSceneActionComposite.png" alt="SceneActionComposite example" width="" height="100">

#### 3. Create an action that destroys a `GameObject` (example)
```csharp
public sealed class DestroyGameObjectSceneAction : SceneActionAbstract<GameObject>
{
    public override void Invoke(GameObject arg) => Destroy(arg);
}
```

#### 4. Assign `DestroyGameObjectSceneAction` to the **Actions** parameter of the `GameObjectSceneActionComposite` component