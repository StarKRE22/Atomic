# 🧩 MonoActionReference

A parameterless reference wrapper for a [MonoAction](MonoAction.md).
Assign a `MonoAction` component in the Inspector and invoke it using `Invoke()`.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [Constructor()](#monoactionreference)
        - [Constructor(MonoAction)](#monoactionreferencemonoactionabstract)
    - [Fields](#-fields)
        - [Action](#action)
    - [Methods](#-methods)
        - [Invoke()](#invoke)

---

## 🗂 Example of Usage

Below is an example of referencing a [MonoActionConfigurable](MonoActionConfigurable.md) with a `HelloWorldMonoAction`.

#### 1. Assume we have a `MonoActionConfigurable` component on a scene

<img src="../../Images/SceneActionReference.png" alt="MonoActionReference non-generic example" width="" height="128">

#### 2. Assume we have an another `HelloWorldMonoAction` on a scene

```csharp
public sealed class HelloWorldMonoAction : MonoAction
{
    public override void Invoke() => Debug.Log("Hello World!");
}
```

#### 3. So we can bind the `HelloWorldMonoAction` to the `MonoActionConfigurable` via

`MonoActionReference` in the Unity Inspector

---

## 🛠 Inspector Settings

| Parameter | Description                             |
|-----------|-----------------------------------------|
| `action`  | Reference to the scene action to invoke |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public sealed class MonoActionReference : IAction
```

- **Description:** A parameterless reference wrapper for a [MonoAction](MonoAction.md).
- **Inheritance:** [IAction](IAction.md)
- **Notes:** Supports Unity serialization and Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

#### `MonoActionReference()`

```csharp
public MonoActionReference();
```

- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `MonoActionReference(MonoAction)`

```csharp
public MonoActionReference(MonoAction action);
```

- **Description:** Creates a new reference wrapping the specified `MonoAction`.
- **Parameter:** `action` — The `MonoAction` to reference.

---

### 🧱 Fields

#### `Action`

```csharp
public MonoAction action;
```

- **Description:** Reference to the scene action to invoke.
- **Access:** Read / Write

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Invokes the referenced scene action if it exists.
