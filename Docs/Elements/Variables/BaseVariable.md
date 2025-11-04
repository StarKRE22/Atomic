# 🧩 Variable&lt;T&gt;

Represents a **simple serialized container** for a value of type `T`.

---

## Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [Variable()](#variable)
    - [Variable(T)](#variablet)
  - [Properties](#-properties)
    - [Value](#value)
  - [Methods](#-methods)
    - [Invoke()](#invoke)
    - [Invoke(T)](#invoket)
    - [ToString()](#tostring)
  - [Operators](#-operators)
    - [Variable<T>(T)](#operator-basevariablett)

---

## 🗂 Example of Usage

```csharp
 // Create a new variable
IVariable<int> score = new Variable<int>(10);

// Read value
Console.WriteLine(score.Value);  // Output: 10

// Write value
score.Value = 20;
Console.WriteLine(score.Value);  // Output: 20
```

---

## 🛠 Inspector Settings

| Parameter | Description                    |
|-----------|--------------------------------|
| `value`   | Current value of this variable |

---

## 🔍 API Reference

### 🏛️ Type <div id="-type"></div>

```csharp
[Serializable]
public class Variable<T> : IVariable<T>
```

- **Description:** Represents a **simple serialized container** for a value of type `T`.
- **Inheritance** [IVariable&lt;T&gt;](IVariable.md)
- **Type Parameter:** `T` – The type of the value to store.
- **Notes:** Support Unity serialization and Odin Inspector
- **See also:** [Base Variables](Variables.md), [ProxyVariable&lt;T&gt;](ProxyVariable.md)

---

<div id="-constructors"></div>

### 🏗️ Constructors

#### `Variable()`

```csharp
public Variable()
```

- **Description:** Initializes a new instance with the default value of `T`.

#### `Variable(T)`

```csharp
public Variable(T value)
```

- **Description:** Initializes a new instance with a specified constant value `value`.
- **Parameter:** `value` – The initial value to initialize the instance with.

---

### 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Gets or sets the current value.
- **Access:** Read-write

---

### 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke()
```

- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;](../Functions/IFunction.md)

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.
- **Notes:**
    - Acts as a setter method, complementing the `Value` property.
    - Default implementation comes from [IAction&lt;T&gt;](../Actions/IAction%601.md).

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the wrapped constant value.
- **Returns:** A string representation of the constant value.

---

### 🪄 Operators

#### `operator Variable<T>(T)`

```csharp
public static implicit operator Variable<T>(T value);
```

- **Description:** Implicitly converts a value of type `T` to a `Variable<T>`.
- **Parameter:** `value` – The value to wrap in a `Variable<T>`.
- **Returns:** A new `Variable<T>` containing the specified value.