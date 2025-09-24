# 🧩 SceneActionAbstract

```csharp
public abstract class SceneActionAbstract : MonoBehaviour, IAction
```

- **Description:** Represents a <b>parameterless</b> scene action that can be invoked.
- **Inheritance:** `MonoBehaviour`, [IAction](IAction.md)
- **Note:** Attach to a GameObject and implement `Invoke()` to define custom behavior.

---

## 🏹 Methods

#### `Invoke()`

```csharp
public abstract void Invoke();
```

- **Description:** Executes the action logic.
- **Note:** Must be implemented in derived classes.

---

## 🗂 Example of Usage

This example demonstrates how to create a simple action based on `SceneActionAbstract` and run it from a `GameStartup`
script in Unity.

#### 1. Create Scene Action

Here we implement a simple action that prints `Hello World!` when invoked:

```csharp
public sealed class HelloWorldAction : SceneActionAbstract
{
    public override void Invoke() => Debug.Log("Hello World!");
}
```

#### 2. Create `GameStartup` Script

This script will call the action on game start:

```csharp
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

#### 3. Assign the Action

- Attach the `GameStartup` script to a GameObject in your scene.
- Drag and drop the `HelloWorldAction` component into the `action` parameter in the Inspector.

#### 4. Run the Scene

When you start the game, the action is triggered and **"Hello World!"** is printed to the console.
