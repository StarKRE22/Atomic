
<details>
  <summary>
    <h2>🧩 InlinePredicate&lt;T&gt;</h2>
    <br> Represents a predicate with <b>one input argument</b> that returns a boolean result.
  </summary>

<br>

```csharp
public class InlinePredicate<T> : InlineFunction<T, bool>, IPredicate<T>
```

- **Type Parameter:** `T` — the type of the input parameter.

---

### 🏗️ Constructors

#### `InlinePredicate(Func<T, bool>)`

```csharp
public InlinePredicate(Func<T, bool> func)
```

- **Description:** Initializes a new instance with the specified function.
- **Parameter:** `func` — the function that takes a `T` and returns a boolean.
- **Throws:** `ArgumentNullException` if `func` is null.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public bool Invoke(T arg)
```

- **Description:** Invokes the function with the provided argument.
- **Parameter:** `arg` — the input parameter.
- **Returns:** The logical result of the function.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of the function.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlinePredicate<T>(Func<T, bool>)`

```csharp
public static implicit operator InlinePredicate<T>(Func<T, bool> value);
```

- **Description:** Implicitly converts a delegate of type `Func<T, bool>` to an `InlinePredicate<T>`.
- **Parameter:** `value` — the delegate to wrap.
- **Returns:** A new `InlinePredicate<T>` containing the specified delegate.

---

### 🗂 Example of Usage

```csharp
Character player = ...
IPredicate<Character> isEnemy = new InlinePredicate<Character>(other => player.Team != other.Team);
bool result = isEnemy.Invoke(enemy);
```

</details>