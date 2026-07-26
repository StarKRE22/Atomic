# 🧩 MonoActionReference&lt;T&gt;

A reference wrapper for a [MonoAction&lt;T&gt;](MonoAction%601.md) with <b>one parameter</b>.
Assign a `MonoAction<T>` component in the Inspector and invoke it using `Invoke()`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [Constructor()](#monoactionreference)
        - [Constructor(MonoAction\<T>)](#monoactionreferencemonoactionabstractt)
    - [Fields](#-fields)
        - [Action](#action)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)

---

## 🗂 Example of Usage

Below is an example of binding `MonoActions` via reference:

#### 1. Assume we have `MonoActionConfigurable` for `GameObject`

```csharp
public sealed class GameObjectMonoActionConfigurable : MonoActionConfigurable<GameObject>
{
}
```

#### 2. Assume we have another `MonoAction<T>` that destroys a game object

```csharp
public sealed class DestroyGameObjectMonoAction : MonoAction<GameObject>
{
    public override void Invoke(GameObject arg) => GameObject.Destroy(arg);
}
```

#### 3. So we can bind the `DestroyGameObjectMonoAction` to the `GameObjectMonoActionConfigurable` via `MonoActionReference` in the Unity Inspector.

<img src="../../Images/GameObjectSceneReference.png" alt="MonoActionReference generic example" width="" height="128">

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
public sealed class MonoActionReference<T> : IAction<T>
```

- **Description:** A reference wrapper for a [MonoAction&lt;T&gt;](MonoAction%601.md) with <b>one
  parameter</b>.
- **Inheritance:** [IAction&lt;T&gt;](IAction%601.md)
- **Type parameter:** `T` — the argument type.
- **Notes:** Supports Unity serialization and Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `MonoActionReference()`

```csharp
public MonoActionReference();
```

- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `MonoActionReference(MonoAction<T>)`

```csharp
public MonoActionReference(MonoAction<T> action);
```

- **Description:** Creates a new reference wrapping the specified `MonoAction<T>`.
- **Parameter:** `action` — The `MonoAction<T>` to reference.

---

### 🧱 Fields

#### `Action`

```csharp
public MonoAction<T> action;
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
