# 🧩 InlineFunction&lt;T, R&gt;

Represents a function with <b>one input argument</b> that returns a result.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [InlineFunction(Func<T, R>)](#inlinefunctionfunct-r)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)
        - [ToString()](#tostring)
    - [Operators](#-operators)
        - [InlineFunction<T, R>(Func<T, R>)](#operator-inlinefunctiont-rfunct-r)

---

## 🗂 Example of Usage

```csharp
// Assume we have instances of some characters
Character player, enemy = ...

// Create the function
IFunction<bool> function = new InlineFunction<Character, bool>(
    other => player.Team != other.Team
);

// Get result
bool isEnemies = function.Invoke(enemy);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineFunction<T, R> : IFunction<T, R>
```

- **Description:** Represents a function with <b>one input argument</b> that returns a result.
- **Type parameters:**
    - `T` — the input parameter type
    - `R` — the return type
- **Inheritance:** [IFunction&lt;T, R&gt;](IFunction%601.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

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