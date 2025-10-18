# 🧩 SceneActionReference&lt;T&gt;

A reference wrapper for a [SceneActionAbstract&lt;T&gt;](SceneActionAbstract%601.md) with <b>one parameter</b>.
Assign a `SceneActionAbstract<T>` component in the Inspector and invoke it using `Invoke()`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [Constructor()](#sceneactionreference)
        - [Constructor(SceneActionAbstract\<T>)](#sceneactionreferencesceneactionabstractt)
    - [Fields](#-fields)
        - [Action](#action)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)

---

## 🗂 Example of Usage

Below is an example of binding `SceneActions` via reference:

#### 1. Assume we have `SceneActionDefault` for `GameObject`

```csharp
public sealed class GameObjectSceneActionDefault : SceneActionDefault<GameObject>
{
}
```

#### 2. Assume we have another `SceneActionAbstract<T>` that destroys a game object

```csharp
public sealed class DestroyGameObjectSceneAction : SceneActionAbstract<GameObject>
{
    public override void Invoke(GameObject arg) => GameObject.Destroy(arg);
}
```

#### 3. So we can bind the `DestroyGameObjectSceneAction` to the `GameObjectSceneActionDefault` via `SceneActionReference` in the Unity Inspector.

<img src="../../Images/GameObjectSceneReference.png" alt="SceneActionReference generic example" width="" height="128">

---

## 🛠 Inspector Settings

| Parameter | Description                           |
|-----------|---------------------------------------|
| `action`  | The referenced scene action to invoke |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public sealed class SceneActionReference<T> : IAction<T>
```

- **Description:** A reference wrapper for a [SceneActionAbstract&lt;T&gt;](SceneActionAbstract%601.md) with <b>one
  parameter</b>.
- **Inheritance:** [IAction&lt;T&gt;](IAction%601.md)
- **Type parameter:** `T` — the argument type.
- **Notes:** Supports Unity serialization and Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

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

### 🧱 Fields

#### `Action`

```csharp
public SceneActionAbstract<T> action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Invokes the referenced scene action with the provided argument.
- **Parameter:** `arg` – The input argument.