# 🧩 SceneActions Reference

**SceneActionReference** is a **pointer** for [SceneActionAbstract](SceneActionsAbstract.md). It is primarily used when
a game designer works with [SceneActionDefault](SceneActionsDefault.md) and needs to reference or invoke another
`SceneAction` from a different context. This wrapper implement the corresponding [IAction](IActions.md) interface
and can be used in **Inspector-driven workflows**.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Non-generic action](#non-generic-action)
    - [Generic action](#generic-action)
- [API Reference](#-api-reference)
- [Notes](#-notes)

---

## 🗂 Examples of Usage

`SceneActionReference` is useful for creating a reference to another `SceneActionAbstract` via `[SerializeReference]`.

### 1️⃣ Non-generic action <div id="non-generic-action"></div>

Below is an example of referencing a `SceneActionDefault` with a `HelloWorldSceneAction`.

<img src="../../Images/SceneActionReference.png" alt="SceneActionReference non-generic example" width="" height="128">

```csharp
public sealed class HelloWorldSceneAction : SceneActionAbstract
{
    public override void Invoke() => Debug.Log("Hello World!");
}
```

---

### 2️⃣ Generic action <div id="generic-action"></div>

Below is an example of referencing a `DestroyGameObjectSceneAction` from the `GameObjectSceneActionDefault`.

<img src="../../Images/GameObjectSceneReference.png" alt="SceneActionReference generic example" width="" height="128">

```csharp
public sealed class GameObjectSceneActionDefault : SceneActionDefault<GameObject>
{
}
```

```csharp
public sealed class DestroyGameObjectSceneAction : SceneActionAbstract<GameObject>
{
    public void Invoke(GameObject arg) => GameObject.Destroy(arg);
}
```

---

## 🔍 API Reference

There are several implementations of reference actions, depending on the number of arguments the actions take:

- [SceneActionReference](SceneActionReference.md) — Non-generic version; works without parameters.
- [SceneActionReference&lt;T&gt;](SceneActionReference%601.md) — Reference that takes one argument.
- [SceneActionReference&lt;T1, T2&gt;](SceneActionReference%602.md) — Reference that takes two arguments.
- [SceneActionReference&lt;T1, T2, T3&gt;](SceneActionReference%603.md) — Reference that takes three arguments.
- [SceneActionReference&lt;T1, T2, T3, T4&gt;](SceneActionReference%604.md) — Reference that takes four arguments.

---

## 📝 Notes

> [!NOTE]  
> The reference only stores a pointer to a `SceneActionAbstract`. If the reference is null, invoking it does nothing.

> [!WARNING]  
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code for clarity
> and maintainability, as `[SerializeReference]` can be fragile during refactoring.