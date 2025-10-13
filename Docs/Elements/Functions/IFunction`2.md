# 🧩 IFunction&lt;T1, T2, R&gt;

Represents a function with <b>two input arguments</b> that returns a result.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Methods](#-methods)
    - [Invoke(T1, T2)](#invoket1-t2)

---

## 🗂 Example of Usage

Below is an example of creating sum function using this interface

```csharp
public class SumFunction : IFunction<int, int, int>
{
    public int Invoke(int a, int b) => a + b;
}
```

Usage:

```csharp
IFunction<int, int, int> func = new SumFunction();
int sum = func.Invoke(3, 4); // sum = 7
```

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
public interface IFunction<in T1, in T2, out R>
```

- **Description:**  Represents a function with <b>two input arguments</b> that returns a result.
- **Type parameters:**
    - `T1` — the first input argument type
    - `T2` — the second input argument type
    - `R` — the return type

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public R Invoke(T1 arg1, T2 arg2);
```

- **Description:** Executes the function with the specified input arguments.
- **Parameters:**
    - `arg1` — the first input argument
    - `arg2` — the second input argument
- **Returns:** The result of type `R`.