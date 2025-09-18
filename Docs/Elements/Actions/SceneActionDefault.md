# 🧩 SceneActionDefault Classes

The `SceneActionDefault` class implements the [IAction](IAction.md) interface and inherits
from [SceneActionAbstract](SceneActionAbstract.md). It allows game designers to build **composite actions directly in
the Unity scene** — chaining multiple `IAction` instances (including generic variants like `IAction<T>`) without writing
additional code.

> [!NOTE]  
> Actions are executed in the order they appear in the array.  
> Null references are automatically skipped, making partially configured lists safe to use.

In essence, `SceneActionDefault` acts as a **container of actions**, executing them sequentially as configured in the *
*Inspector** through `[SerializeReference]`.

> [!WARNING]
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code instead for
> clarity and maintainability, because `[SerializeReference]` is very fragile during refactoring.

---

<details>
  <summary>
    <h2>🧩 SceneActionDefault</h2>
    <br> Represents a <b>parameterless</b> composite scene action that can be invoked.
  </summary>

<br>

```csharp
public class SceneActionDefault : SceneActionAbstract
```

- **Usage:** Attach to a `GameObject`, assign a list of `IAction` implementations in the `Inspector`, and they will be
  invoked sequentially.

---

### 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

### 🧱Fields

#### `actions`

```csharp
public IAction[] actions;
```

- **Description:** The array of actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke()`

```csharp
public override void Invoke();
```

- **Description:** Executes each action in the `actions` array sequentially.

</details>

---

<details>
  <summary>
    <h2>🧩 SceneActionDefault&lt;T&gt;</h2>
    <br> Represents a scene-based composite action with <b>one parameter</b>.
  </summary>

<br>

```csharp
public abstract class SceneActionDefault<T> : SceneActionAbstract<T>
```

- **Type parameter:** `T` — the input argument type.

---

### 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

### 🧱Fields

#### `actions`

```csharp
public IAction<T>[] actions;
```

- **Description:** The array of actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T arg)`

```csharp
public override void Invoke(T arg);
```

- **Description:** Executes each action sequentially with the provided argument.

</details>

---

<details>
  <summary>
    <h2>🧩 SceneActionDefault&lt;T1, T2&gt;</h2>
    <br> Represents a scene-based composite action with <b>two parameters</b>.
  </summary>

<br>

```csharp
public abstract class SceneActionDefault<T1, T2> : SceneActionAbstract<T1, T2>
```

- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument

---

### 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

### 🧱Fields

#### `actions`

```csharp
public IAction<T1, T2>[] actions;
```

- **Description:** The array of actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2)`

```csharp
public override void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes each action sequentially with the provided arguments.

</details>

---

<details>
  <summary>
    <h2>🧩 SceneActionDefault&lt;T1, T2, T3&gt;</h2>
    <br> Represents a scene-based composite action with <b>three parameters</b>.
  </summary>

<br>

```csharp
public abstract class SceneActionDefault<T1, T2, T3> : SceneActionAbstract<T1, T2, T3>
```

- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument

---

### 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

### 🧱Fields

#### `actions`

```csharp
public IAction<T1, T2, T3>[] actions;
```

- **Description:** The array of actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`

```csharp
public override void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes each action sequentially with the provided arguments.

</details>

---

<details>
  <summary>
    <h2>🧩 SceneActionDefault&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents a scene-based composite action with <b>four parameters</b>.
  </summary>

<br>

```csharp
public abstract class SceneActionDefault<T1, T2, T3, T4> : SceneActionAbstract<T1, T2, T3, T4>
```

- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

---

### 🛠 Inspector Settings

| Parameter | Description                              |
|-----------|------------------------------------------|
| `actions` | The array of actions to execute in order |

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)`

```csharp
public override void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Executes each action sequentially with the provided arguments.

</details>

---

## 🗂 Example of Usage

For **narrative or scenario-driven games**, where designers need to configure a lot of actions directly on the scene,
`SceneAction` combined with `[SerializeReference]` is very convenient.

---

### 🔹 Non-generic Usage

Below is an example of using `SceneActionDefault`

#### 1. Add the `Atomic/Elements/Action` component.

<img src="../../Images/SceneAction.png" alt="SceneAction example" width="384" height="137">

#### 2. In the **Inspector**, assign the `PrintAction` value to the `Action` parameter.

#### 3. Use `SceneActionDefault` as `SceneActionAbstract` in your components.

```csharp
// Example of usage "SceneActionDefault"
public sealed class GameStartup : MonoBehaviour
{
    [SerializeField] private SceneActionAbstract _startup;

    private void Start() => _startup.Invoke();
}
```

---

### 🔹 Generic Usage

Below is an example of using `SceneActionDefault<T>` with a `GameObject`.

#### 1. Create a `GameObjectSceneActionDefault` component

```csharp
using Atomic.Elements;
using UnityEngine;

public sealed class GameObjectSceneActionDefault : SceneActionDefault<GameObject>
{
}
```

#### 2. Add the `GameObjectSceneActionDefault` component to a `GameObject`

<img src="../../Images/GameObjectSceneActionDefault.png" alt="GameObjectSceneActionDefault component" width="380" height="74">

#### 3. Create an action that destroys a `GameObject` (example)

```csharp
[Serializable]
public sealed class DestroyGameObjectAction : IAction<GameObject>
{
    public void Invoke(GameObject arg) => GameObject.Destroy(arg);
}
```

#### 4. Assign `DestroyGameObjectAction` to the **Actions** parameter of the `GameObjectSceneActionDefault` component

<img src="../../Images/GameObjectSceneActionDefault_WithAction.png" alt="GameObjectSceneActionDefault with Destroy action" height="95">