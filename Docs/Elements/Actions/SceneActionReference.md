# 🧩 SceneActionReference

```csharp
[Serializable]
public sealed class SceneActionReference : IAction
```

- **Description:** A parameterless reference wrapper for a [SceneActionAbstract](SceneActionAbstract.md).
- **Inheritance:** [IAction](IAction.md)
- **Notes:** Supports Unity serialization and Odin Inspector
- **Usage:** Assign a `SceneActionAbstract` component in the Inspector and invoke it using `Invoke()`.

---

## 🛠 Inspector Settings

| Parameter | Description                             |
|-----------|-----------------------------------------|
| `action`  | Reference to the scene action to invoke |

---

## 🏗️ Constructors

#### `SceneActionReference()`

```csharp
public SceneActionReference();
```

- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract)`

```csharp
public SceneActionReference(SceneActionAbstract action);
```

- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract`.
- **Parameter:** `action` — The `SceneActionAbstract` to reference.

---

## 🧱 Fields

#### `action`

```csharp
public SceneActionAbstract action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write

---

## 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Invokes the referenced scene action if it exists.

---

## 🗂 Example of Usage

Below is an example of referencing a `SceneActionDefault` with a `HelloWorldSceneAction`.

<img src="../../Images/SceneActionReference.png" alt="SceneActionReference non-generic example" width="" height="128">

```csharp
public sealed class HelloWorldSceneAction : SceneActionAbstract
{
    public override void Invoke() => Debug.Log("Hello World!");
}
```