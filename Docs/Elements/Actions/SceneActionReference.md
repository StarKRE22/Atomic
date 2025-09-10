# 🧩 SceneActionReference Classes

The `SceneActionReference` is **pointer** for [SceneActionAbstract](SceneActionAbstract.md). It is primarily used when a game designer works with [SceneActionDefault](SceneActionDefault.md) and needs to reference or invoke another `SceneActionDefault` from a different context. This wrapper implement the corresponding [IAction](IAction.md) interface and can be used in **Inspector-driven workflows**.

> [!NOTE]  
> The reference only stores a pointer to a `SceneActionAbstract`. If the reference is null, invoking it does nothing.

---

## 🧩 SceneActionReference
```csharp
public sealed class SceneActionReference : IAction
```
- **Description:** A parameterless reference wrapper for a `SceneActionAbstract`.
- **Usage:** Assign a `SceneActionAbstract` component in the Inspector and invoke it using `Invoke()`.

### Inspector Settings

| Parameter | Type                  | Description                                      |
|-----------|----------------------|--------------------------------------------------|
| `action`  | `SceneActionAbstract` | Reference to the scene action to invoke         |

### Constructors

#### `SceneActionReference()`
```csharp
public SceneActionReference();
```
- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract action)`
```csharp
public SceneActionReference(SceneActionAbstract action);
```
- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract`.
- **Parameters:**
    - `action` — The `SceneActionAbstract` to reference.

### Methods

#### `Invoke()`
```csharp
public void Invoke();
```
- **Description:** Invokes the referenced scene action if it exists.

---

## 🧩 SceneActionReference&lt;T&gt;

```csharp
public sealed class SceneActionReference<T> : IAction<T>
```
- **Description:** Reference wrapper for a scene action with **one parameter**.
- **Type parameter:** `T` — the argument type.

### Inspector Settings

| Parameter | Type                     | Description                                 |
|-----------|--------------------------|---------------------------------------------|
| `action`  | `SceneActionAbstract<T>` | The referenced scene action to invoke       |

### Constructors

#### `SceneActionReference()`
```csharp
public SceneActionReference();
```
- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract<T> action)`
```csharp
public SceneActionReference(SceneActionAbstract<T> action);
```
- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract<T>`.
- **Parameters:**
    - `action` — The `SceneActionAbstract<T>` to reference.

### Methods

#### `Invoke(T arg)`
```csharp
public void Invoke(T arg);
```
- **Description:** Invokes the referenced scene action with the provided argument.

---

## 🧩 SceneActionReference&lt;T1, T2&gt;
```csharp
public sealed class SceneActionReference<T1, T2> : IAction<T1, T2>
```
- **Description:** Reference wrapper for a scene action with **two parameters**.
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument

### Inspector Settings

| Parameter | Type                        | Description                          |
|-----------|-----------------------------|--------------------------------------|
| `action`  | `SceneActionAbstract<T1,T2>` | The referenced scene action to invoke |


### Constructors

#### `SceneActionReference()`
```csharp
public SceneActionReference();
```
- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract<T1, T2> action)`
```csharp
public SceneActionReference(SceneActionAbstract<T1, T2> action);
```
- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract<T1, T2>`.
- **Parameters:**
    - `action` — The `SceneActionAbstract<T1, T2>` to reference.


### Methods

#### `Invoke(T1 arg1, T2 arg2)`
```csharp
public void Invoke(T1 arg1, T2 arg2);
```
- **Description:** Invokes the referenced scene action with the provided arguments.

---

## 🧩 SceneActionReference&lt;T1, T2, T3&gt;
```csharp
public sealed class SceneActionReference<T1, T2, T3> : IAction<T1, T2, T3>
```
- **Description:** Reference wrapper for a scene action with **three parameters**.
- **Type parameters:** `T1`, `T2`, `T3` — the arguments.

### Inspector Settings

| Parameter | Type                            | Description                          |
|-----------|---------------------------------|--------------------------------------|
| `action`  | `SceneActionAbstract<T1,T2,T3>` | The referenced scene action to invoke |

### Constructors

#### `SceneActionReference()`
```csharp
public SceneActionReference();
```
- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract<T1, T2, T3> action)`
```csharp
public SceneActionReference(SceneActionAbstract<T1, T2, T3> action);
```
- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract<T1, T2, T3>`.
- **Parameters:**
  - `action` — The `SceneActionAbstract<T1, T2, T3>` to reference.

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`
```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```
- **Description:** Invokes the referenced scene action with the provided arguments.

---

## 🧩 SceneActionReference&lt;T1, T2, T3, T4&gt;
```csharp
public sealed class SceneActionReference<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
```
- **Description:** Reference wrapper for a scene action with **four parameters**.
- **Type parameters:** `T1`, `T2`, `T3`, `T4` — the arguments.

### Inspector Settings

| Parameter | Type                                  | Description                          |
|-----------|---------------------------------------|--------------------------------------|
| `action`  | `SceneActionAbstract<T1,T2,T3,T4>`    | The referenced scene action to invoke |

### Constructors

#### `SceneActionReference()`
```csharp
public SceneActionReference();
```
- **Description:** Default constructor, intended **only for use by the Unity Inspector**.
- **Usage:** Required for Unity to serialize the reference in the Inspector.

#### `SceneActionReference(SceneActionAbstract<T1, T2, T3, T4> action)`
```csharp
public SceneActionReference(SceneActionAbstract<T1, T2, T3, T4> action);
```
- **Description:** Creates a new reference wrapping the specified `SceneActionAbstract<T1, T2, T3, T4>`.
- **Parameters:**
  - `action` — The `SceneActionAbstract<T1, T2, T3, T4>` to reference.

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)`
```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```
- **Description:** Invokes the referenced scene action with the provided arguments.

---
## 🗂 Example of Usage

**`SceneActionReference` is useful for creating a reference to another `SceneActionAbstract` via `[SerializeReference]`.**

---

### 🔹 Non-generic Example

Below is an example of referencing a `SceneActionDefault` with a `HelloWorldSceneAction`.

<img src="../../Images/SceneActionReference.png" alt="SceneActionReference non-generic example" width="" height="128">

---

### 🔹 Generic Example

Below is an example of referencing a `DestroyGameObjectSceneAction` from the `GameObjectSceneActionDefault`.

<img src="../../Images/GameObjectSceneReference.png" alt="SceneActionReference generic example" width="" height="128">

> [!WARNING]  
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code for clarity and maintainability, as `[SerializeReference]` can be fragile during refactoring.
