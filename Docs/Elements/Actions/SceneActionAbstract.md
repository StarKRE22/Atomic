# 🧩 SceneActionAbstract Classes

The **SceneActionAbstract** classes define **scene-based actions** in Unity that implement the corresponding [IAction](IAction.md) interfaces.  

These abstract classes inherit from `MonoBehaviour`, allowing actions to be attached to GameObjects in a scene.

They serve as a base for **custom scene logic** and are designed to be subclassed to implement specific behavior.

> [!NOTE]
> Extremely useful for cutscenes, trigger-based actions, level initialization, and similar scene-driven logic.

---

## 🧩 SceneActionAbstract

```csharp
public abstract class SceneActionAbstract : MonoBehaviour, IAction
```
- **Description:** Represents a **parameterless scene action**.
- **Usage:** Attach to a GameObject and implement `Invoke()` to define custom behavior.

### Methods

#### `Invoke()`
```csharp
public abstract void Invoke();
```
- **Description:** Executes the action logic.
- **Note:** Must be implemented in derived classes.

### 🗂 Example of Usage

```csharp
public class HelloWorldAction : SceneActionAbstract
{
    public void Invoke() => Console.WriteLine("Hello World!");
}
```
```csharp
// Usage #1
public sealed class GameStartup : MonoBehaviour
{
    [SerializeField]
    private SceneActionAbstract _action;
    
    private void Start()
    {
        _action.Invoke();
    }
}
```

```csharp
// Usage #2
IAction action = gameObject.GetComponent<IAction>;
action.Invoke(); // Output: Hello World!
```
---

## 🧩 SceneActionAbstract&lt;T&gt;
```csharp
public abstract class SceneActionAbstract<T> : MonoBehaviour, IAction<T>
```
- **Description:** Represents a scene action with **one parameter**.
- **Type parameter:** `T` — the input argument type.

### Methods

#### `Invoke(T arg)`
```csharp
public abstract void Invoke(T arg);
```
- **Description:** Executes the action logic with the provided argument.

### 🗂 Example of Usage

```csharp
public class DestroyGameObjectAction : SceneActionAbstract<GameObject>
{
    public void Invoke(GameObject go) => GameObject.Destroy(go);
}
```
```csharp
public sealed class ActionTrigger : MonoBehaviour
{
    [SerializeField]
    private SceneActionAbstract _action;
    
    private void OnTriggerEnter(Collider collider)
    {
        _action.Invoke(collider.gameObject);
    }
}
```

---

## 🧩 SceneActionAbstract<T1, T2>
```csharp
public abstract class SceneActionAbstract<T1, T2> : MonoBehaviour, IAction<T1, T2>
```
- **Description:** Represents a scene action with **two parameters**.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument

### Methods

#### `Invoke(T1 arg1, T2 arg2)`
```csharp
public abstract void Invoke(T1 arg1, T2 arg2);
```
- **Description:** Executes the action logic with the provided arguments.

### 🗂 Example of Usage
```csharp
public sealed class DealDamageAction : SceneActionAbstract<Character, int>
{
    public void Invoke(Character character, int damage) => character.TakeDamage(damage);
}
```

---

## 🧩 SceneActionAbstract<T1, T2, T3>
```csharp
public abstract class SceneActionAbstract<T1, T2, T3> : MonoBehaviour, IAction<T1, T2, T3>
```
- **Description:** Represents a scene action with **three parameters**.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`
```csharp
public abstract void Invoke(T1 arg1, T2 arg2, T3 arg3);
```
- **Description:** Executes the action logic with the provided arguments.

### 🗂 Example of Usage

```csharp
public sealed class MoveResourcesAction : SceneActionAbstract<Storage, Storage, int>
{
    public void Invoke(Storage source, Storage destination, int amount)
    {
        source.SpendResources(amount);
        destination.EarnResources(amount);
    }
}
```

---

## 🧩 SceneActionAbstract<T1, T2, T3, T4>
```csharp
public abstract class SceneActionAbstract<T1, T2, T3, T4> : MonoBehaviour, IAction<T1, T2, T3, T4>
```
- **Description:** Represents a scene action with **four parameters**.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
    - `T3` — the third argument
    - `T4` — the fourth argument

### Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)`
```csharp
public abstract void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```
- **Description:** Executes the action logic with the provided arguments.


### 🗂 Example of Usage

```csharp
public class MoveTransformAction : SceneActionAbstract<Transform, Vector3, float, float>
{
    public void Invoke(Transform transform, Vector3 direction, float speed, float deltaTime) => 
        transform.position += direction * (speed * deltaTime);
}
```