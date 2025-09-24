# 🧩 IAction&lt;T&gt;

```csharp
public interface IAction<in T>
```

- **Description:** Represents an executable action that <b>takes one argument</b>.
- **Type parameter:** `T` — the input parameter

---

## 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Executes the action with the specified argument
- **Parameter:** `arg` — the input parameter

---

## 🗂 Example of Usage

```csharp
public sealed class DestroyGameObjectAction : IAction<GameObject>
{
    public void Invoke(GameObject go) 
    {
        GameObject.Destroy(go);  
    } 
}
```
```csharp
// Assume we have a GameObject
GameObject gameObject = ...

// Destroy object
IAction<GameObject> action = new DestroyGameObjectAction();
action.Invoke(gameObject);
```
