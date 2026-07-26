# 🧩 MonoActions Configurable

Implement the [IAction](IActions.md) interfaces and inherit
from [MonoAction](MonoActions.md). It allows game designers to build **composite actions directly in
the Unity scene** — chaining multiple action instances, including generic variants, without writing
additional code.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic action](#non-generic-action)
    - [Generic action](#generic-action)
- [API Reference](#-api-reference)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

For **narrative or scenario-driven games**, where designers need to configure a lot of actions directly on the scene,
`MonoAction` combined with `[SerializeReference]` is very convenient.

---

### 1️⃣ Non-generic action <div id="non-generic-action"></div>

Below is an example of using [MonoActionConfigurable](MonoActionConfigurable.md)

#### 1. Add the `Atomic/Elements/Action` component.

<img src="../../Images/SceneAction.png" alt="MonoAction example" width="384" height="137">

#### 2. In the **Inspector**, assign the [PrintAction](PrintAction.md) value to the `Action` parameter.

#### 3. Use [MonoActionConfigurable](MonoActionConfigurable.md) as [MonoAction](MonoAction.md) in your components.

```csharp
// Example of usage "MonoActionConfigurable"
public sealed class GameStartup : MonoBehaviour
{
    [SerializeField] 
    private MonoAction _startup;

    private void Start() => _startup.Invoke();
}
```

---

### 2️⃣ Generic action <div id="generic-action"></div>

Below is an example of using `MonoActionConfigurable<T>` with a `GameObject`.

#### 1. Create a `GameObjectMonoActionConfigurable` component

```csharp
using Atomic.Elements;
using UnityEngine;

public sealed class GameObjectMonoActionConfigurable : MonoActionConfigurable<GameObject>
{
}
```

#### 2. Add the `GameObjectMonoActionConfigurable` component to a `GameObject`

<img src="../../Images/GameObjectSceneActionDefault.png" alt="GameObjectSceneActionDefault component" width="380" height="74">

#### 3. Create an action that destroys a `GameObject` (example)

```csharp
[Serializable]
public sealed class DestroyGameObjectAction : IAction<GameObject>
{
    public void Invoke(GameObject arg) => GameObject.Destroy(arg);
}
```

#### 4. Assign `DestroyGameObjectAction` to the **Actions** parameter of the `GameObjectMonoActionConfigurable` component

<img src="../../Images/GameObjectSceneActionDefault_WithAction.png" alt="GameObjectSceneActionDefault with Destroy action" height="95">

---

## 🔍 API Reference

There are several implementations of default scene actions, depending on the number of arguments the actions take:

- [MonoActionConfigurable](MonoActionConfigurable.md) — Non-generic version; works without parameters.
- [MonoActionConfigurable&lt;T&gt;](MonoActionConfigurable%601.md) — Action that takes one argument.
- [MonoActionConfigurable&lt;T1, T2&gt;](MonoActionConfigurable%602.md) — Action that takes two arguments.
- [MonoActionConfigurable&lt;T1, T2, T3&gt;](MonoActionConfigurable%603.md) — Action that takes three arguments.
- [MonoActionConfigurable&lt;T1, T2, T3, T4&gt;](MonoActionConfigurable%604.md) — Action that takes four arguments.

---

## 📝 Notes

> [!NOTE]  
> Actions are executed in the order they appear in the array.  
> Null references are automatically skipped, making partially configured lists safe to use.

> [!TIP]
> In essence, **MonoActionConfigurable** acts as a **container of actions**, executing them sequentially as configured in
> the **Inspector** through `[SerializeReference]`.

> [!WARNING]
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code instead for
> clarity and maintainability, because `[SerializeReference]` is very fragile during refactoring.
