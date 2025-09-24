
<details>
  <summary>
    <h2>🧩 IAction&lt;T1, T2&gt;</h2>
    <br> Represents an executable action that <b>takes two arguments</b>.
  </summary>

<br>

```csharp
public interface IAction<in T1, in T2>
```

- **Type parameters:**
    - `T1` — the first argument
    - `T2` — the second argument

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes the action with the specified arguments
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument

---

### 🗂 Example of Usage

```csharp
public sealed class DealDamageAction : IAction<Character, int>
{
    public void Invoke(Character character, int damage) => character.TakeDamage(damage);
}

// Usage
IAction<Character, int> action = new DealDamageAction();
action.Invoke(enemy, 5);
```

</details>