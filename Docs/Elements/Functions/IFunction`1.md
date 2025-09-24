
<details>
  <summary>
    <h2>🧩 IFunction&lt;T, R&gt;</h2>
    <br> Represents a function with <b>one input argument</b> that returns a result.
  </summary>

<br>

```csharp
public interface IFunction<in T, out R>
```

- **Type parameters:**
    - `T` — the input argument type
    - `R` — the return type

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public R Invoke(T arg);
```

- **Description:** Executes the function with the specified input argument.
- **Parameter:** `arg` — the input argument.
- **Returns:** The result of type `R`.

---

### 🗂 Example of Usage

```csharp
public sealed class IsEnemyFunction : IFunction<Character, bool>
{
    private readonly Character _source;
    
    public IsEnemyFunction(Character source) 
    {
        _source = source;  
    } 
    
    public bool Invoke(Character other) 
    {
        return _source.Team != other.Team; 
    } 
}
```

```csharp
//Usage
IFunction<Character, bool> func = new IsEnemyFunction(character);
bool isEnemies = func.Invoke(otherCharacter);
```

</details>