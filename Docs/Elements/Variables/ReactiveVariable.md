# 🧩 ReactiveVariable&lt;T&gt;

Represents a **serialized reactive variable** that raises events whenever its value changes.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
- [Inspector Settings](#-inspector-settings)
- [API Reference](#-api-reference)
  - [Type](#-type)
  - [Constructors](#-constructors)
    - [ReactiveVariable()](#reactivevariable)
    - [ReactiveVariable(T)](#reactivevariablet)
  - [Events](#-events)
    - [OnEvent](#onevent)
  - [Properties](#-properties)
    - [Value](#value)
  - [Methods](#-methods)
    - [Invoke()](#invoke)
    - [Invoke(T)](#invoket)
    - [Dispose](#dispose)
    - [ToString()](#tostring)
  - [Operators](#-operators)
    - [ReactiveVariable<T>(T)](#operator-reactivevariablett)

---

## 🗂 Example of Usage

```csharp
// Initialize with a starting value
var score = new ReactiveVariable<int>(10);

// Subscribe to changes
score.Subscribe(newValue => Console.WriteLine("Score updated: " + newValue));

// Change the value
score.Value = 20; // Triggers subscription callback

 // Dispose to clear subscriptions
score.Dispose();
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
public class ReactiveVariable<T> : IReactiveVariable<T>, IDisposable
```

- **Description:** Represents a **serialized reactive variable** that raises events whenever its value changes.
- **Inheritance:** [IReactiveVariable&lt;T&gt;](IReactiveVariable.md), `IDisposable`,
- **Type Parameter:** `T` – The type of the value.
- **Notes:** Support Unity serialization and Odin Inspector
- **See also:** [Reactive Variables](ReactiveVariables.md)

---

<div id="-constructors"></div>

### 🏗️ Constructors

#### `ReactiveVariable()`

```csharp
public ReactiveVariable()
```

- **Description:** Initializes a new instance with the default value of `T`.

#### `ReactiveVariable(T)`

```csharp
public ReactiveVariable(T value)
```

- **Description:** Initializes a new instance with a specified constant value `value`.
- **Parameter:** `value` – The initial value to initialize the instance with.

---

### ⚡ Events

#### `OnEvent`

```csharp
event Action<T> OnEvent
```

- **Description:** Triggered whenever the value changes.
- **Parameter**: `T` – The new value after the change.
- **Note:** Allows subscribers to react to value changes in a reactive programming pattern.

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

- **Description:** Invokes the variable and returns its current value.
- **Returns:** The current value of type `T`.

#### `Invoke(T)`

```csharp
public void Invoke(T arg)
```

- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.

#### `Dispose`

```csharp
public void Dispose()
```

- **Description:** Clears all listeners and releases resources.

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the wrapped constant value.
- **Returns:** A string representation of the constant value.

--- 

### 🪄 Operators

#### `operator ReactiveVariable<T>(T)`

```csharp
public static implicit operator ReactiveVariable<T>(T value);
```

- **Description:** Implicitly converts a value of type `T` to a `ReactiveVariable<T>`.
- **Parameter:** `value` – The value to wrap in a `ReactiveVariable<T>`.
- **Returns:** A new `ReactiveVariable<T>` containing the specified value.