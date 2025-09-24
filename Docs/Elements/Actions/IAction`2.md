# 🧩 IAction&lt;T1, T2&gt;

```csharp
public interface IAction<in T1, in T2>
```

- **Description:** Represents an executable action that <b>takes two arguments</b>.
- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument

---

## 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes the action with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument

---

## 🗂 Example of Usage

```csharp
public sealed class DealDamageAction : IAction<Character, int>
{
    public void Invoke(Character character, int damage) 
    {
        character.TakeDamage(damage);
    } 
}
```

```csharp
// Assume we have an enemy instance
Character enemy = ...

// Deal damage
IAction<Character, int> action = new DealDamageAction();
action.Invoke(enemy, 5);
```
