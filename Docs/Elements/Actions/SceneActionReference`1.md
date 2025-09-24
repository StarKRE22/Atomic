# 🧩 SceneActionReference&lt;T&gt;

```csharp
[Serializable]
public sealed class SceneActionReference<T> : IAction<T>
```

- **Description:** A reference wrapper for a [SceneActionAbstract&lt;T&gt;](SceneActionAbstract%601.md) with <b>one parameter</b>.
- **Inheritance:** [IAction&lt;T&gt;](IAction%601.md)
- **Type parameter:** `T` — the argument type.
- **Notes:** Supports Unity serialization and Odin Inspector
- **Usage:** Assign a `SceneActionAbstract<T>` component in the Inspector and invoke it using `Invoke()`.

---

## 🛠 Inspector Settings

| Parameter | Description                           |
|-----------|---------------------------------------|
| `action`  | The referenced scene action to invoke |

---

## 🏗️ Constructors

#### `SceneActionReference()`

```csharp
public SceneActionReference();
```

- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract<T>)`

```csharp
public SceneActionReference(SceneActionAbstract<T> action);
```

- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract<T>`.
- **Parameter:** `action` — The `SceneActionAbstract<T>` to reference.

---

## 🧱 Fields

#### `action`

```csharp
public SceneActionAbstract<T> action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write

---

## 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Invokes the referenced scene action with the provided argument.
- **Parameter:** `arg` – The input argument.

---

## 🗂 Example of Usage

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
    public override void Invoke(GameObject arg) => GameObject.Destroy(arg);
}
```