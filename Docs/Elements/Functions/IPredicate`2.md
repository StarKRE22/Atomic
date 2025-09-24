
<details>
  <summary>
    <h2>🧩 IPredicate&lt;T1, T2&gt;</h2>
    <br> Represents a predicate with <b>two input arguments</b> that returns a boolean result.
  </summary>

<br>

```csharp
public interface IPredicate<in T1, in T2> : IFunction<T1, T2, bool>
```

- **Type parameters:**
    - `T1` — the first input argument type
    - `T2` — the second input argument type

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public bool Invoke(T1 arg1, T2 arg2);
```

- **Description:** Evaluates the predicate with the specified arguments.
- **Parameters:**
    - `arg1` — the first input argument
    - `arg2` — the second input argument
- **Returns:** `true` or `false` based on the predicate logic.

---

### 🗂 Example of Usage

```csharp
public class AreAlliesPredicate : IPredicate<Character, Character>
{
    public bool Invoke(Character a, Character b) => a.Team == b.Team;
}
```

```csharp
//Usage
IPredicate<Character, Character> predicate = new AreAlliesPredicate();
bool areAllies = predicate.Invoke(characterA, characterB);
```

</details>