# 🧩 SceneActionAbstract Classes

The **SceneActionAbstract** classes define **scene-based actions** in Unity that implement the corresponding [IAction](IAction.md) interfaces.
These abstract classes inherit from `MonoBehaviour`, allowing actions to be attached to GameObjects in a scene.
They serve as a base for **custom scene logic** and are designed to be subclassed to implement specific behavior.

> [!NOTE]
> Extremely useful for cutscenes, trigger-based actions, level initialization, and similar scene-driven logic.

---

<details>
  <summary>
    <h2>🧩 SceneActionAbstract</h2>
    <br> Represents a <b>parameterless</b> scene action that can be invoked.
  </summary>

<br>

```csharp
public abstract class SceneActionAbstract : MonoBehaviour, IAction
```
- **Description:** Represents a **parameterless scene action**.
- **Usage:** Attach to a GameObject and implement `Invoke()` to define custom behavior.

### 🏹 Methods

#### `Invoke()`
```csharp
public abstract void Invoke();
```
- **Description:** Executes the action logic.
- **Note:** Must be implemented in derived classes.

### 🗂 Example of Usage

This example demonstrates how to create a simple action based on `SceneActionAbstract` and run it from a `GameStartup` script in Unity.

#### 1. Create a custom action
Here we implement a simple action that prints `Hello World!` when invoked:

```csharp
public sealed class HelloWorldAction : SceneActionAbstract
{
    public override void Invoke() => Debug.Log("Hello World!");
}
```

#### 2. Create the `GameStartup` script
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

#### 3. Assign the action in the Unity Inspector
- Attach the `GameStartup` script to a GameObject in your scene.
- Drag and drop the `HelloWorldAction` component into the `action` parameter in the Inspector.

#### 4. Run the scene
When you start the game, the action is triggered and **"Hello World!"** is printed to the console.

</details>

---

<details>
  <summary>
    <h2>🧩 SceneActionAbstract&lt;T&gt;</h2>
    <br> Represents a scene action with <b>one parameter</b> that can be invoked.
  </summary>

<br>

```csharp
public abstract class SceneActionAbstract<T> : MonoBehaviour, IAction<T>
```
- **Type parameter:** `T` — the input argument type.

### 🏹 Methods

#### `Invoke(T arg)`
```csharp
public abstract void Invoke(T arg);
```
- **Description:** Executes the action logic with the provided argument.

### 🗂 Example of Usage
This example shows how to use `SceneActionAbstract<T>` to create an action that destroys objects when they enter a trigger.

#### 1. Create `DestroyGameObjectAction`
This action takes a `GameObject` and destroys it:

```csharp
public sealed class DestroyGameObjectAction : SceneActionAbstract<GameObject>
{
    public override void Invoke(GameObject go) => GameObject.Destroy(go);
}
```

#### 2. Create `ActionTrigger`
This script invokes the action whenever another object enters the trigger collider:

```csharp
public sealed class ActionTrigger : MonoBehaviour
{
    [SerializeField]
    private SceneActionAbstract<GameObject> _action;

    private void OnTriggerEnter(Collider collider)
    {
        _action.Invoke(collider.gameObject);
    }
}
```

#### 3. Run the scene
Enter **Play Mode** in Unity and any objects that collide with the trigger will be **destroyed automatically**.

</details>

---

<details>
  <summary>
    <h2>🧩 SceneActionAbstract&lt;T1, T2&gt;</h2>
    <br> Represents a scene action with <b>two parameters</b> that can be invoked.
  </summary>

<br>

```csharp
public abstract class SceneActionAbstract<T1, T2> : MonoBehaviour, IAction<T1, T2>
```
- **Type parameters:**
  - `T1` — the first argument
  - `T2` — the second argument

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2)`
```csharp
public abstract void Invoke(T1 arg1, T2 arg2);
```
- **Description:** Executes the action logic with the provided arguments.

### 🗂 Example of Usage
This example shows how to use `SceneActionAbstract<T1, T2>` to apply damage to a character.

#### 1. Create `DealDamageAction`
This action takes a **character** and a **damage value**, then applies the damage:

```csharp
public sealed class DealDamageAction : SceneActionAbstract<Character, int>
{
    public override void Invoke(Character character, int damage)
        => character.TakeDamage(damage);
}
```

#### 2. Usage in Gameplay
- Attach the `DealDamageAction` to a GameObject.
- Call `Invoke(targetCharacter, damageAmount)` when you want to apply damage (for example, when an enemy attacks or the player steps into a trap).

#### 3. Result
The specified character’s `TakeDamage` method will be executed, reducing its health.

</details>

---

<details>
  <summary>
    <h2>🧩 SceneActionAbstract&lt;T1, T2, T3&gt;</h2>
    <br> Represents a scene action with <b>three parameters</b> that can be invoked.
  </summary>

<br>

```csharp
public abstract class SceneActionAbstract<T1, T2, T3> : MonoBehaviour, IAction<T1, T2, T3>
```
- **Description:** Represents a scene action with **three parameters**.
- **Type parameters:**
  - `T1` — the first argument
  - `T2` — the second argument
  - `T3` — the third argument

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`
```csharp
public abstract void Invoke(T1 arg1, T2 arg2, T3 arg3);
```
- **Description:** Executes the action logic with the provided arguments.

### 🗂 Example of Usage

This example shows how to use `SceneActionAbstract<T1, T2, T3>` with multiple parameters to transfer resources between two `Storage` components.

#### 1. Create `MoveResourcesAction`
This action takes a **source storage**, a **destination storage**, and an **amount** of resources to move:

```csharp
public sealed class MoveResourcesAction : SceneActionAbstract<Storage, Storage, int>
{
    public override void Invoke(Storage source, Storage destination, int amount)
    {
        source.SpendResources(amount);
        destination.EarnResources(amount);
    }
}
```

#### 2. Usage in Gameplay
- Attach the `MoveResourcesAction` to a GameObject.
- Call `Invoke(source, destination, amount)` when you want to transfer resources.

For example, when a player collects items or trades between inventories, the resources will be deducted from one storage and added to another.

</details>

---

<details>
  <summary>
    <h2>🧩 SceneActionAbstract&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents a scene action with <b>four parameters</b> that can be invoked.
  </summary>

<br>

```csharp
public abstract class SceneActionAbstract<T1, T2, T3, T4> : MonoBehaviour, IAction<T1, T2, T3, T4>
```
- **Description:** Represents a scene action with **four parameters**.
- **Type parameters:**
  - `T1` — the first argument
  - `T2` — the second argument
  - `T3` — the third argument
  - `T4` — the fourth argument

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)`
```csharp
public abstract void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```
- **Description:** Executes the action logic with the provided arguments.

### 🗂 Example of Usage

This example demonstrates how to move a `Transform` in a given direction with a specified speed and deltaTime.

#### 1. Create `MoveTransformAction`
This action takes a **Transform**, a **direction vector**, a **speed**, and **deltaTime**, then moves the Transform accordingly:

```csharp
public sealed class MoveTransformAction : SceneActionAbstract<Transform, Vector3, float, float>
{
    public void Invoke(Transform transform, Vector3 direction, float speed, float deltaTime) => 
        transform.position += direction * (speed * deltaTime);
}
```

#### 2. Usage in Gameplay
- Attach the `MoveTransformAction` to a GameObject.
- Call `Invoke(transform, direction, speed, deltaTime)` in an update loop or event to move the object over time.

#### 3. Result
The GameObject’s position will be updated every frame according to the specified direction and speed, allowing smooth movement.

</details>
