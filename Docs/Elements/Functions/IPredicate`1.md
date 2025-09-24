
<details>
  <summary>
    <h2>🧩 IPredicate&lt;T&gt;</h2>
    <br> Represents a predicate with <b>one input argument</b> that returns a boolean result.
  </summary>

<br>

```csharp
public interface IPredicate<in T> : IFunction<T, bool>
```

- **Type parameter:** `T` — the input argument type.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public bool Invoke(T arg);
```

- **Description:** Evaluates the predicate with the specified argument.
- **Parameter:** `arg` — the input argument.
- **Returns:** `true` or `false` based on the predicate logic.

---

### 🗂 Example of Usage

```csharp
public class IsEnemyPredicate : IPredicate<Character>
{
    private readonly Character _source;

    public IsEnemyPredicate(Character source) => _source = source;
    
    public bool Invoke(Character other) => _source.Team != other.Team;
}
```

```csharp
//Usage
IPredicate<Character> predicate = new IsEnemyPredicate(character);
bool isEnemy = predicate.Invoke(otherCharacter);
```

</details>