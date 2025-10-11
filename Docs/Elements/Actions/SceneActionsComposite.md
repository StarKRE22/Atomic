# 🧩 SceneActions Composite

The **SceneActionComposite** classes represent a **group** of [SceneActionAbstract](SceneActionsAbstract.md) instances that
can be invoked sequentially. It follows the [Composite Pattern](https://en.wikipedia.org/wiki/Composite_pattern): the
group itself behaves as a single scene action, while internally invoking all contained scene actions in order.

> [!TIP]
> This class is ideal for **building complex scene behaviors** directly in the Unity Inspector without writing extra
> code. Actions are executed in the order they appear in the array. Null references are automatically skipped, making
> partially configured lists safe to use.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic action](#non-generic-action)
    - [Generic action](#generic-action)
- [API Reference](#-api-reference)

---

## 🗂 Example of Usage

`SceneActionComposite` can be used similarly to [SceneActionDefault](SceneActionsDefault.md) but is **strictly a
composite container for `SceneActionAbstract`**.

### 1️⃣ Non-generic action <div id="non-generic-action"></div>

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

### 2️⃣ Generic action <div id="generic-action"></div>

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

---

## 🔍 API Reference

There are several implementations of composite scene actions, depending on the number of arguments the actions take:

- [SceneActionComposite](SceneActionComposite.md) — Non-generic version; works without parameters.
- [SceneActionComposite&lt;T&gt;](SceneActionComposite%601.md) — Action that takes one argument.
- [SceneActionComposite&lt;T1, T2&gt;](SceneActionComposite%602.md) — Action that takes two arguments.
- [SceneActionComposite&lt;T1, T2, T3&gt;](SceneActionComposite%603.md) — Action that takes three arguments.
- [SceneActionComposite&lt;T1, T2, T3, T4&gt;](SceneActionComposite%604.md) — Action that takes four arguments.