# 🧩 SceneActionDefault

The **SceneActionDefault** class implements the [IAction](IAction.md) interface and inherits from [SceneActionAbstract](SceneActionAbstract.md). It allows game designers to build **composite actions directly in the Unity scene** — chaining multiple `IAction` instances (including generic variants like `IAction<T>`) without writing additional code.

In essence, `SceneActionDefault` acts as a **container of actions**, executing them sequentially as configured in the **Inspector** through `[SerializeReference]`.

> [!NOTE]  
> Actions are executed in the order they appear in the array.  
> Null references are automatically skipped, making partially configured lists safe to use.

> [!WARNING]
> Using `[SerializeReference]` should be considered a last resort. If possible, define actions through code instead for clarity and maintainability, because `[SerializeReference]` is very fragile during refactoring.

---

## 🧩 SceneActionDefault
```csharp
public class SceneActionDefault : SceneActionAbstract
```
- **Description:** Represents a **parameterless composite scene action**.
- **Usage:** Attach to a `GameObject`, assign a list of `IAction` implementations in the `Inspector`, and they will be invoked sequentially.

### Inspector Settings

| Parameter | Type        | Description                             |
|-----------|-------------|-----------------------------------------|
| `actions` | `IAction[]` | The array of actions to execute in order|

### Methods

#### `Invoke()`
```csharp
public override void Invoke();
```
- **Description:** Executes each action in the `actions` array sequentially.

---

## 🧩 SceneActionDefault&lt;T&gt;
```csharp
public abstract class SceneActionDefault<T> : SceneActionAbstract<T>
```
- **Description:** Represents a scene-based composite action with **one parameter**.
- **Type parameter:** `T` — the input argument type.

### Inspector Settings

| Parameter | Type        | Description                             |
|-----------|-------------|-----------------------------------------|
| `actions` | `IAction[]` | The array of actions to execute in order|

### Methods

#### `Invoke(T arg)`
```csharp
public override void Invoke(T arg);
```
- **Description:** Executes each action sequentially with the provided argument.

---

## 🧩 SceneActionDefault&lt;T1, T2&gt;
```csharp
public abstract class SceneActionDefault<T1, T2> : SceneActionAbstract<T1, T2>
```
- **Description:** Represents a scene-based composite action with **two parameters**.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument

### Fields
- `IAction<T1, T2>[] actions` — the array of actions to execute.

### Methods

#### `Invoke(T1 arg1, T2 arg2)`
!!!
public override void Invoke(T1 arg1, T2 arg2);
!!!
- **Description:** Executes each action sequentially with the provided arguments.

---

## 🧩 SceneActionDefault&lt;T1, T2, T3&gt;

!!!
public abstract class SceneActionDefault<T1, T2, T3> : SceneActionAbstract<T1, T2, T3>
!!!
- **Description:** Represents a scene-based composite action with **three parameters**.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument

### Fields
- `IAction<T1, T2, T3>[] actions` — the array of actions to execute.

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`
!!!
public override void Invoke(T1 arg1, T2 arg2, T3 arg3);
!!!
- **Description:** Executes each action sequentially with the provided arguments.

---

## 🧩 SceneActionDefault&lt;T1, T2, T3, T4&gt;

!!!
public abstract class SceneActionDefault<T1, T2, T3, T4> : SceneActionAbstract<T1, T2, T3, T4>
!!!
- **Description:** Represents a scene-based composite action with **four parameters**.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

### Fields
- `IAction<T1, T2, T3, T4>[] actions` — the array of actions to execute.

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)`
!!!
public override void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
!!!
- **Description:** Executes each action sequentially with the provided arguments.


## 🗂 Example of Usage

For **narrative or scenario-driven games**, where designers need to configure a lot of actions directly on the scene, `SceneAction` combined with `[SerializeReference]` is very convenient.

### Non-generic usage

<img src="../../Images/SceneAction.png" alt="img.png" width="384" height="137">

1. Add `Atomic/Elements/Action` component.
2. For example in the **Inspector**, we can assign the `PrintAction` value to the `Action` parameter:
3. You can use `SceneActionDefault` as `SceneActionAbstract` in your components 
```csharp
//Example of usage "SceneActionDefault"
public sealed class GameStartup : MonoBehaviour
{
    [SerializeField] 
    private SceneActionAbstract _startup;

    private void Start() => _startup.Invoke();
}
```

### Generic usage


