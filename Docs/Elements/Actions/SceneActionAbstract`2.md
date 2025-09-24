# 🧩 SceneActionAbstract&lt;T1, T2&gt;

```csharp
public abstract class SceneActionAbstract<T1, T2> : MonoBehaviour, IAction<T1, T2>
```

- **Description:** Represents a scene action with <b>two parameters</b> that can be invoked.
- **Inheritance:** `MonoBehaviour`, [IAction&lt;T1, T2&gt;](IAction%602.md)
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument
- **Note:** Attach to a GameObject and implement `Invoke(T1, T2)` to define custom behavior.

---

## 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public abstract void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes the action logic with the provided arguments.
- **Parameters:**
    - `arg1` – The first argument
    - `arg2` – The second argument

---

## 🗂 Example of Usage

This example shows how to use `SceneActionAbstract<T1, T2>` to apply damage to a character.

```csharp
public sealed class DealDamageAction : SceneActionAbstract<Character, int>
{
    public override void Invoke(Character character, int damage)
    {
        character.TakeDamage(damage);
    }
}
```