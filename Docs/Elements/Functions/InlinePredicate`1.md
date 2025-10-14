# 🧩 InlinePredicate&lt;T&gt;

Represents a predicate with <b>one input argument</b> that returns a boolean result.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [InlinePredicate(Func<T, bool>)](#inlinepredicatefunct-bool)
    - [Methods](#-methods)
        - [Invoke(T)](#invoket)
        - [ToString()](#tostring)
    - [Operators](#-operators)
        - [InlinePredicate<T>(Func<T, bool>)](#operator-inlinepredicatetfunct-bool)

---

## 🗂 Example of Usage

```csharp
// Assume we have instances of some Character class
Character player, enemy = ...
    
// Create the predicate
IPredicate<Character> predicate = new InlinePredicate<Character>(
    other => player.Team != other.Team
);

// Get the result
bool isEnemy = predicate.Invoke(enemy);
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlinePredicate<T> : InlineFunction<T, bool>, IPredicate<T>
```

- **Description:** Represents a predicate with <b>one input argument</b> that returns a boolean result.
- **Type Parameter:** `T` — the type of the input parameter.
- **Inheritance:** [InlineFunction&lt;T, R&gt;](InlineFunction%601.md), [IPredicate&lt;T&gt;](IPredicate%601.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

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