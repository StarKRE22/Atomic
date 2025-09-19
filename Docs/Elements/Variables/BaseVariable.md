# 🧩 BaseVariable&lt;T&gt;

Represents a **simple serialized container** for a value of type `T`. It implements [IVariable&lt;T&gt;](IVariable.md),
providing **read-write access** to the stored value.

```csharp
public class BaseVariable<T> : IVariable<T>
```

- **Type Parameter:** `T` – The type of the value to store.

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
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;.Invoke()](../Functions/IFunction.md#invoke)

#### `Invoke(T arg)`

```csharp
public void Invoke(T arg)
```

- **Description:** Sets the value of the variable to the provided argument.
- **Parameter:** `arg` – The new value to assign to the variable.
- **Notes:**
    - Acts as a setter method, complementing the `Value` property.
    - Default implementation comes from [IAction&lt;T&gt;.Invoke()](../Actions/IAction.md#invoket).

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the wrapped constant value.
- **Returns:** A string representation of the constant value.

---

## 🪄 Operators

#### `operator BaseVariable<T>(T value)`

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

---

## 🧩 Specialized Variables

For convenience, several specialized implementations of base variables are provided. It is recommended to use them, as
they compare values without relying on `EqualityComparer`, which makes them slightly faster than the generic
`BaseVariable<T>` version.

### 🧩 Common Types

- `BoolVariable` – Boolean variable
- `IntVariable` – Integer variable
- `FloatVariable` – Float variable

### 🧩 Unity Types

- `QuaternionVariable` – Stores a `Quaternion`
- `Vector2Variable` – Stores a `Vector2`
- `Vector3Variable` – Stores a `Vector3`
- `Vector4Variable` – Stores a `Vector4`
- `Vector2IntVariable` – Stores a `Vector2Int`
- `Vector3IntVariable` – Stores a `Vector3Int`

### 🧩 Unity Mathematics Types

- `int2_variable` – Stores an `int2`
- `int3_variable` – Stores an `int3`
- `int4_variable` – Stores an `int4`
- `float2_variable` – Stores a `float2`
- `float3_variable` – Stores a `float3`
- `float4_variable` – Stores a `float4`
- `quaternion_variable` – Stores a `quaternion`