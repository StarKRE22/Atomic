
<details>
  <summary>
    <h2>🧩 InlineFunction&lt;T, R&gt;</h2>
    <br> Represents a function with <b>one input argument</b> that returns a result.
  </summary>

<br>

```csharp
public class InlineFunction<T, R> : IFunction<T, R>
```

- **Type parameters:**
    - `T` — the input parameter type
    - `R` — the return type

---

### 🏗️ Constructors

#### `InlineFunction(Func<T, R>)`

```csharp
public InlineFunction(Func<T, R> func)
```

- **Description:** Initializes a new instance with the specified function delegate.
- **Parameter:** `func` — the function to invoke.
- **Throws:** `ArgumentNullException` if `func` is null.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public R Invoke(T args)
```

- **Description:** Invokes the function with the provided argument.
- **Parameter:** `args` — the input parameter.
- **Returns:** The result of the function.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of function.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineFunction<T, R>(Func<T, R>)`

```csharp
public static implicit operator InlineFunction<T, R>(Func<T, R> value);
```

- **Description:** Implicitly converts a delegate of type `Func<T, R>` to an `InlineFunction<T, R>`.
- **Parameter:** `value` — the delegate to wrap.
- **Returns:** A new `InlineFunction<T, R>` containing the specified delegate.

---

### 🗂 Example of Usage

```csharp
Character player = ...
IFunction<bool> isEnemies = new InlineFunction<Character, bool>(other => player.Team != other.Team);

//Usage
Character enemy = ...
isEnemies.Invoke(enemy);
```

</details>