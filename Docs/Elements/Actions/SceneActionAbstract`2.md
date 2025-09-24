
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

---

### 🏹 Methods

#### `Invoke(T1 arg1, T2 arg2)`

```csharp
public abstract void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes the action logic with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument

---

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
- Call `Invoke(targetCharacter, damageAmount)` when you want to apply damage (for example, when an enemy attacks or the
  player steps into a trap).

#### 3. Result

The specified character’s `TakeDamage` method will be executed, reducing its health.

</details>