# 🧩 BaseVariable&lt;T&gt;

```csharp
[Serializable]
public class BaseVariable<T> : IVariable<T>
```

- **Description:** Represents a **simple serialized container** for a value of type `T`.
- **Inheritance** [IVariable&lt;T&gt;](IVariable.md)
- **Type Parameter:** `T` – The type of the value to store.
- **Notes:** Support Unity serialization and Odin Inspector

---

## 🛠 Inspector Settings

| Parameter | Description                    |
|-----------|--------------------------------|
| `value`   | current value of this variable |

---

## 🏗️ Constructors

#### `BaseVariable()`

```csharp
public BaseVariable()
```

- **Description:** Initializes a new instance with the default value of `T`.

#### `BaseVariable(T)`

```csharp
public BaseVariable(T value)
```

- **Description:** Initializes a new instance with a specified constant value `value`.
- **Parameter:** `value` – The initial value to initialize the instance with.

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { get; set; }
```

- **Description:** Gets or sets the current value.
- **Access:** Read-write

---

## 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke()
```

- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;](../Functions/IFunction.md)

#### `Invoke(T arg)`

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

## 🪄 Operators

#### `operator BaseVariable<T>(T)`

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