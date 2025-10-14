# 🧩 InlineFunction&lt;R&gt;

Represents a <b>parameterless</b> function that returns a result.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [InlineFunction(Func<R>)](#inlinefunctionfuncr)
    - [Properties](#-properties)
        - [Value](#value)
    - [Methods](#-methods)
        - [Invoke()](#invoke)
        - [ToString()](#tostring)
    - [Operators](#-operators)
        - [InlineFunction<R>(Func<R>)](#operator-inlinefunctionrfuncr)

---

## 🗂 Example of Usage

```csharp
// Assume we have an instance of GameObject
GameObject gameObject = ...

// Create the inline function    
IFunction<bool> function = new InlineFunction<bool>(
    () => gameObject.activeSelf
);

// Get result
bool activeSelf = function.Invoke();
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineFunction<R> : IFunction<R>, IValue<R>
```

- **Description:** Represents a <b>parameterless</b> function that returns a result.
- **Type parameter:** `R` — the return type
- **Inheritance:** [IFunction&lt;R&gt;](IFunction.md), [IValue&lt;T&gt;](../Values/IValue.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

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