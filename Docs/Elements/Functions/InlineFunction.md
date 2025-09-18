# 🧩 InlineFunction Classes

The **InlineFunction** classes provide wrappers around standard `System.Func` delegates. They implement the
corresponding [IValue](../Values/IValue.md) and [IFunction](IFunction.md) interfaces and allow invoking functions
directly, optionally with parameters.

They also support implicit conversion from the underlying `Func` delegates.

---

<details>
  <summary>
    <h2>🧩 InlineFunction&lt;R&gt;</h2>
    <br> Represents a <b>parameterless</b> function that returns a result.
  </summary>

<br>

- **Type parameter:** `R` — the return type

---

### 🏗️ Constructors

#### `InlineFunction(Func<R>)`

```csharp
public InlineFunction(Func<R> func)
```

- **Description:** Initializes a new instance with the specified function delegate.
- **Parameter:** `func` — the function to invoke.
- **Throws:** `ArgumentNullException` if `func` is null.

--- 

### 🔑 Properties

#### `Value`

```csharp
public T Value { get; }
```

- **Description:** Invokes the wrapped function and returns the result.
- **Returns:** The result of type `R`.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public R Invoke()
```

- **Description:** Invokes the function and returns its result.
- **Returns:** The result of the function.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of function.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineFunction<R>(Func<R>)`

```csharp
public static implicit operator InlineFunction<R>(Func<R> value);
```

- **Description:** Implicitly converts a delegate of type `Func<R>` to an `InlineFunction<R>`.
- **Parameter:** `value` — the delegate to wrap.
- **Returns:** A new `InlineFunction<R>` containing the specified delegate.

---

### 🗂 Example of Usage

```csharp
GameObject gameObject = ...
IFunction<bool> function = new InlineFunction<bool>(() => gameObject.activeSelf);
function.Invoke();
```

</details>

---

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

---

<details>
  <summary>
    <h2>🧩 InlineFunction&lt;T1, T2, R&gt;</h2>
    <br> Represents a function with <b>two input arguments</b> that returns a result.
  </summary>

<br>

```csharp
public class InlineFunction<T1, T2, R> : IFunction<T1, T2, R>
```

- **Type parameters:**
    - `T1` — the first input parameter type
    - `T2` — the second input parameter type
    - `R` — the return type

---

### 🏗️ Constructors

#### `InlineFunction(Func<T1, T2, R>)`

```csharp
public InlineFunction(Func<T1, T2, R> func)
```

- **Description:** Initializes a new instance with the specified function delegate.
- **Parameter:** `func` — the function to invoke.
- **Throws:** `ArgumentNullException` if `func` is null.

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public R Invoke(T1 arg1, T2 arg2)
```

- **Description:** Invokes the function with the provided arguments.
- **Parameters:**
    - `arg1` — the first argument
    - `arg2` — the second argument
- **Returns:** The result of the function.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the method name of function.
- **Returns:** A string representation of the method name of delegate.

---

### 🪄 Operators

#### `operator InlineFunction<T1, T2, R>(Func<T1, T2, R>)`

```csharp
public static implicit operator InlineFunction<T1, T2, R>(Func<T1, T2, R> value);
```

- **Description:** Implicitly converts a delegate of type `Func<T1, T2, R>` to an `InlineFunction<T1, T2, R>`.
- **Parameter:** `value` — the delegate to wrap.
- **Returns:** A new `InlineFunction<T1, T2, R>` containing the specified delegate.

---

### 🗂 Example of Usage

```csharp
IFunction<int, int, int> sumFunc = new InlineFunction<int, int, int>((a, b) => a + b);
int sum = sumFunc.Invoke(3, 4); // sum = 7
```

</details>

---

## 📌 Best Practice

`InlineFunction` is ideal for creating functions for specific game objects using **lambda expressions**, making it
easy to define custom behavior inline for values, computations, or reactive systems.

Below is an example of using `InlineFunction` to provide dynamic values for different entities.

```csharp
//Using position and rotation from Rigidbody
var entity = new Entity("Tank");
entity.AddPosition(new InlineFunction<Vector3>(() => rigidbody.position));
entity.AddRotation(new InlineFunction<Quaternion>(() => rigidbody.rotation));
```

```csharp
//Using position and rotation from Transform
var entity = new Entity("Ship");
entity.AddPosition(new InlineFunction<Vector3>(() => transform.position));
entity.AddRotation(new InlineFunction<Quaternion>(() => transform.rotation));
```