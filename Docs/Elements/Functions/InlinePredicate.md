# 🧩 InlinePredicate Classes

The **InlinePredicate** classes provide wrappers around standard `System.Func` delegates that return a boolean value. It extends from [InlineFunction](InlineFunction.md) and implementa the corresponding [Predicate](IPredicate.md) interfaces and allow invoking predicates directly, optionally with parameters.

They also support implicit conversion from the underlying `Func` delegates and can be used inline for conditions, validations, or reactive checks.

---

## 🧩 InlinePredicate
```csharp
public class InlinePredicate : InlineFunction<bool>, IPredicate
```
- **Description:** Represents a **parameterless boolean predicate**.

### Constructors

#### `InlinePredicate(Func<bool> func)`
```csharp
public InlinePredicate(Func<bool> func)
```
- **Description:** Initializes a new instance with the specified boolean-returning function.
- **Parameter:** `func` — the function to invoke.
- **Throws:** `ArgumentNullException` if `func` is null.

### Properties

#### `Value`
```csharp
public T Value { get; }
```
- **Description:** Invokes the wrapped function and returns the result.
- **Returns:** The result of type `T`.

### Methods

#### `Invoke()`
```csharp
public bool Invoke()
```
- **Description:** Invokes the function and returns boolean result.
- **Returns:** The logical result of the function.

#### `ToString()`
```csharp
public override string ToString();
```
- **Description:** Returns a string that represents the method name of function.
- **Returns:** A string representation of the method name of delegate.

### Operators

#### `implicit operator InlinePredicate(Func<bool>)`
```csharp
public static implicit operator InlinePredicate(Func<bool> value);
```
- **Description:** Implicitly converts a delegate of type `Func<bool>` to an `InlinePredicate`.
- **Parameter:** `value` — the delegate to wrap.
- **Returns:** A new `InlinePredicate` containing the specified delegate.

### 🗂 Example of Usage
```csharp
GameObject gameObject = ...
IPredicate predicate = new InlinePredicate(() => gameObject.activeSelf);
bool result = predicate.Invoke();
```
---

## 🧩 InlinePredicate&lt;T, R&gt;
```csharp
public class InlinePredicate<T> : InlineFunction<T, bool>, IPredicate<T>
```
- **Description:** Represents a **boolean predicate with one input parameter**.
- **Type Parameter:** `T` — the type of the input parameter.

### Constructors

#### `InlinePredicate(Func<T, bool> func)`
```csharp
public InlinePredicate(Func<T, bool> func)
```
- **Description:** Initializes a new instance with the specified function.
- **Parameter:** `func` — the function that takes a `T` and returns a boolean.
- **Throws:** `ArgumentNullException` if `func` is null.

### Methods

#### `Invoke(T arg)`
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

### Operators

#### `implicit operator InlinePredicate<T>(Func<T, bool>)`
```csharp
public static implicit operator InlinePredicate<T>(Func<T, bool> value);
```
- **Description:** Implicitly converts a delegate of type `Func<T, bool>` to an `InlinePredicate<T>`.
- **Parameter:** `value` — the delegate to wrap.
- **Returns:** A new `InlinePredicate<T>` containing the specified delegate.

### 🗂 Example of Usage
```csharp
Character player = ...
IPredicate<Character> isEnemy = new InlinePredicate<Character>(other => player.Team != other.Team);
bool result = isEnemy.Invoke(enemy);
```
---

## 🧩 InlinePredicate<T1, T2>
```csharp
public class InlinePredicate<T1, T2> : InlineFunction<T1, T2, bool>, IPredicate<T1, T2>
`````
- **Description:** Represents a **boolean predicate with two input parameters**.
- **Type Parameters:**
  - `T1` — the first input type
  - `T2` — the second input type

### Constructors

#### `InlinePredicate(Func<T1, T2, bool> func)`
```csharp
public InlinePredicate(Func<T1, T2, bool> func)
````
- **Description:** Initializes a new instance with the specified function.
- **Parameter:** `func` — the function that takes `T1` and `T2` and returns a boolean.
- **Throws:** `ArgumentNullException` if `func` is null.

### Methods

#### `Invoke(T1 arg1, T2 arg2)`
```csharp
public bool Invoke(T1 arg1, T2 arg2)
````
- **Description:** Invokes the function with the provided arguments.
- **Parameters:**
  - `arg1` — the first input parameter
  - `arg2` — the second input parameter
- **Returns:** The logical result of the function.

#### `ToString()`
```csharp
public override string ToString();
````
- **Description:** Returns a string that represents the method name of the function.
- **Returns:** A string representation of the method name of delegate.

### Operators

#### `implicit operator InlinePredicate<T1, T2>(Func<T1, T2, bool>)`
```csharp
public static implicit operator InlinePredicate<T1, T2>(Func<T1, T2, bool> value);
````
- **Description:** Implicitly converts a delegate of type `Func<T1, T2, bool>` to an `InlinePredicate<T1, T2>`.
- **Parameter:** `value` — the delegate to wrap.
- **Returns:** A new `InlinePredicate<T1, T2>` containing the specified delegate.

### 🗂 Example of Usage
```csharp
Character player = ...
IPredicate<Character, Character> isEnemyPair = new InlinePredicate<Character, Character>((a, b) => a.Team != b.Team);
bool result = isEnemyPair.Invoke(player, enemy);
````