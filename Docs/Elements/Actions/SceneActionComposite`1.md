
<details>
  <summary>
    <h2>🧩 SceneActionComposite&lt;T&gt;</h2>
    <br> Represents a composite scene action with <b>one parameter</b> that can be invoked.
  </summary>

<br>

```csharp
public class SceneActionComposite<T> : SceneActionAbstract<T>
```

- **Type parameter:** `T` — the argument type.

---

### 🛠 Inspector Settings

| Parameter | Description                                                      |
|-----------|------------------------------------------------------------------|
| `actions` | The array of scene actions to invoke in order  with one argument |

---

### 🧱Fields

#### `actions`

```csharp
public SceneActionAbstract<T>[] actions;
```

- **Description:** The array of scene actions to invoke in order.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T arg)`

```csharp
public override void Invoke(T arg);
```

- **Description:** Executes each action sequentially with the provided argument.
- **Parameter:** `arg` – The input argument.

</details>


## 🗂 Example of Usage

`SceneActionComposite` can be used similarly to [SceneActionDefault](SceneActionDefault.md) but is **strictly a
composite container for `SceneActionAbstract`**.

---

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

---

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

#### 4. Assign `DestroyGameObjectSceneAction` to the **Actions** parameter of the
`GameObjectSceneActionComposite` component