
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

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2, T3 arg3)`

```csharp
public abstract void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Executes the action logic with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument
    - `arg3` – The third argument

---

### 🗂 Example of Usage

This example shows how to use `SceneActionAbstract<T1, T2, T3>` with multiple parameters to transfer resources between
two `Storage` components.

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

For example, when a player collects items or trades between inventories, the resources will be deducted from one storage
and added to another.

</details>