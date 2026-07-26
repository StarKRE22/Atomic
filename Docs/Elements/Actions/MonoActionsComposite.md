# 🧩 MonoActions Composite

The **MonoActionComposite** classes represent a **group** of [MonoAction](MonoActions.md) instances that
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

`MonoActionComposite` can be used similarly to [MonoActionConfigurable](MonoActionsConfigurable.md) but is **strictly a
composite container for `MonoAction`**.

### 1️⃣ Non-generic action <div id="non-generic-action"></div>

#### 1. Add the `Atomic/Elements/Action Composite` component to a `GameObject`.

<img src="../../Images/SceneActionComposite.png" alt="MonoActionComposite example" width="" height="100">

#### 2. Assign `HelloWorldSceneAction` component to the **Actions** array in the Inspector.

```csharp
public sealed class HelloWorldSceneAction : MonoAction
{
    public override void Invoke() => Debug.Log("Hello world");
}
```

---

### 2️⃣ Generic action <div id="generic-action"></div>

#### 1. Create a `GameObjectMonoActionComposite` component.

```csharp
using Atomic.Elements;
using UnityEngine;

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

There are several implementations of composite scene actions, depending on the number of arguments the actions take:

- [MonoActionComposite](MonoActionComposite.md) — Non-generic version; works without parameters.
- [MonoActionComposite&lt;T&gt;](MonoActionComposite%601.md) — Action that takes one argument.
- [MonoActionComposite&lt;T1, T2&gt;](MonoActionComposite%602.md) — Action that takes two arguments.
- [MonoActionComposite&lt;T1, T2, T3&gt;](MonoActionComposite%603.md) — Action that takes three arguments.
- [MonoActionComposite&lt;T1, T2, T3, T4&gt;](MonoActionComposite%604.md) — Action that takes four arguments.
