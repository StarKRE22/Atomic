# 🧩 BaseVariable&lt;T&gt;

`BaseVariable<T>` is a **simple serialized container** for a value of type `T`. It implements [IVariable&lt;T&gt;](IVariable.md), providing **read-write access** to the stored value.

---

## Type Parameter

- `T` – The type of the value to store.

---


## Constructors

#### `BaseVariable()`
```csharp
// Default constructor
public BaseVariable()
```
- **Description:** Initializes a new instance with the default value of `T`.

#### `BaseVariable(T value)`
```csharp
public BaseVariable(T value)
```
- **Description:** Initializes a new instance with a specified constant value `value`.
- **Parameter:** - `value` – The constant value to initialize the instance with.

## Properties

#### `Value`
```csharp
new T Value { get; set; }
```
- **Description:** Gets or sets the current value.
- **Access:** Read-write

## Methods

#### `Invoke()`
```csharp
T Invoke()
```
- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;.Invoke()](../Functions/IFunction.md#invoke)

#### `ToString()`
```csharp
public override string ToString();
```
- **Description:** Returns a string that represents the wrapped constant value.
- **Returns:** A string representation of the constant value.


## Operators

#### `implicit operator BaseVariable<T>(T value)`
```csharp
public static implicit operator BaseVariable<T>(T value);
```
- **Description:** Implicitly converts a value of type `T` to a `BaseVariable<T>`.
- **Parameter:** `value` – The value to wrap in a `BaseVariable<T>`.
- **Returns:** A new `BaseVariable<T>` containing the specified value.

---

## 🗂 Example of Usage
```csharp
 // Create a new variable
IVariable<int> score = new BaseVariable<int>(10);

// Read value
Console.WriteLine(score.Value);  // Output: 10

// Write value
score.Value = 20;
Console.WriteLine(score.Value);  // Output: 20
```