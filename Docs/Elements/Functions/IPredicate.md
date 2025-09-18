# 🧩 IPredicate

The **IPredicate** interfaces are specialized forms of [functions](IFunction.md) that evaluate a condition and return
`true` or `false`, useful in filtering, validation, and decision-making logic.

---

<details>
  <summary>
    <h2>🧩 IPredicate</h2>
    <br> Represents a <b>parameterless</b> predicate that returns a boolean result.
  </summary>

<br>

```csharp
public interface IPredicate : IFunction<bool>
```

---

### 🏹 Methods

#### `Invoke()`

```csharp
public bool Invoke();
```

- **Description:** Evaluates the predicate and returns a boolean result.
- **Returns:** `true` or `false` based on the predicate logic.

---

### 🗂 Example of Usage

```csharp
public class IsGameActivePredicate : IPredicate
{
    private readonly GameManager _manager;

    public IsGameActivePredicate(GameManager manager) => _manager = manager;
    
    public bool Invoke() => _manager.IsActive;
}
```

```csharp
//Usage
IPredicate predicate = new IsGameActivePredicate(gameManager);
bool isActive = predicate.Invoke();
```

</details>

---

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

---

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
