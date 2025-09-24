# 🧩 SceneActions Reference

**SceneActionReference** is a **pointer** for [SceneActionAbstract](SceneActionsAbstract.md). It is primarily used when
a
game designer works with [SceneActionDefault](SceneActionsDefault.md) and needs to reference or invoke another
`SceneActionDefault` from a different context. This wrapper implement the corresponding [IAction](IActions.md) interface
and can be used in **Inspector-driven workflows**.

> [!NOTE]  
> The reference only stores a pointer to a `SceneActionAbstract`. If the reference is null, invoking it does nothing.

There are several implementations of reference actions, depending on the number of arguments the actions take:

- [SceneActionReference](SceneActionReference.md) — Non-generic version; works without parameters.
- [SceneActionReference&lt;T&gt;](SceneActionReference%601.md) — Reference that take one argument.
- [SceneActionReference&lt;T1, T2&gt;](SceneActionReference%602.md) — Reference that take two arguments.
- [SceneActionReference&lt;T1, T2, T3&gt;](SceneActionReference%603.md) — Reference that take three arguments.
- [SceneActionReference&lt;T1, T2, T3, T4&gt;](SceneActionReference%604.md) — Reference that take four arguments.

---

## 🗂 Examples of Usage

`SceneActionReference` is useful for creating a reference to another `SceneActionAbstract` via `[SerializeReference]`.

> [!WARNING]  
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code for clarity
> and maintainability, as `[SerializeReference]` can be fragile during refactoring.

---

### 🔹 Non-generic Example

Below is an example of referencing a `SceneActionDefault` with a `HelloWorldSceneAction`.

<img src="../../Images/SceneActionReference.png" alt="SceneActionReference non-generic example" width="" height="128">

```csharp
public sealed class HelloWorldAction : SceneActionAbstract
{
    public override void Invoke() => Debug.Log("Hello World!");
}
```

---

### 🔹 Generic Example

Below is an example of referencing a `DestroyGameObjectSceneAction` from the `GameObjectSceneActionDefault`.

<img src="../../Images/GameObjectSceneReference.png" alt="SceneActionReference generic example" width="" height="128">

```csharp
public sealed class GameObjectSceneActionDefault : SceneActionDefault<GameObject>
{
}
```

```csharp
[Serializable]
public sealed class DestroyGameObjectAction : IAction<GameObject>
{
    public void Invoke(GameObject arg) => GameObject.Destroy(arg);
}
```