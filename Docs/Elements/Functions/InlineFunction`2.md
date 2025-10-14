# 🧩 InlineFunction&lt;T1, T2, R&gt;

Represents a function with <b>two input arguments</b> that returns a result.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
    - [Type](#-type)
    - [Constructors](#-constructors)
        - [InlineFunction(Func<T1, T2, R>)](#inlinefunctionfunct1-t2-r)
    - [Methods](#-methods)
        - [Invoke(T1, T2)](#invoket1-t2)
        - [ToString()](#tostring)
    - [Operators](#-operators)
        - [InlineFunction<T1, T2, R>(Func<T1, T2, R>)](#operator-inlinefunctiont1-t2-rfunct1-t2-r)

---

## 🗂 Example of Usage

```csharp
IFunction<int, int, int> function = new InlineFunction<int, int, int>(
    (a, b) => a + b
);

int sum = function.Invoke(3, 4); // 7
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public class InlineFunction<T1, T2, R> : IFunction<T1, T2, R>
```

- **Description:** Represents a function with <b>two input arguments</b> that returns a result.
- **Type parameters:**
    - `T1` — the first input parameter type
    - `T2` — the second input parameter type
    - `R` — the return type
- **Inheritance:** [IFunction&lt;T1, T2, R&gt;](IFunction%602.md)
- **Note:** Supports Odin Inspector

---

### 🏗️ Constructors <div id="-constructors"></div>

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