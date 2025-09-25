# 🧩 Const&lt;T&gt;

```csharp
[Serializable]
public class Const<T> : IValue<T>
```

- **Description:** Represents a **serialized, immutable (read-only) constant value wrapper**.
- **Inheritance:** [IValue&lt;T&gt;](IValue.md)
- **Type Parameter:** `T` – The type of the wrapped constant value.
- **Remarks:**
    - Supports **implicit conversions** for convinience
    - Supports Unity serialization
    - Supports Odin Inspector

> [!TIP]
> Unlike regular value types, `Const<T>` is a **reference type**, making it lightweight to pass around. It can act as
> a [Flyweight pattern](https://en.wikipedia.org/wiki/Flyweight_pattern), for example, to share a constant value across
> multiple instances without copying the value.

---

## 🏗️ Constructors

#### `Const()`

```csharp
public Const()
```

- **Description:** Initializes a new instance with the default value of `T`.
- **Note:** Default constructor.

#### `Const(T)`

```csharp
public Const(T value)
```

- **Description:** Initializes a new instance with a specified constant value `value`.
- **Parameter:** - `value` – The constant value to initialize the instance with.

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { get; }
```

- **Description:** Gets the wrapped constant value.
- **Access:** Read-only

---

## 🏹 Methods

#### `Invoke()`

```csharp
public T Invoke()
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

---

## 🪄 Operators

#### `operator Const<T>(T)`

```csharp
public static implicit operator Const<T>(T value);
```

- **Description:** Implicitly converts a value of type `T` to a `Const<T>`.
- **Parameter:** `value` – The value to wrap in a `Const<T>`.
- **Returns:** A new `Const<T>` containing the specified value.

#### `operator T(Const<T>)`

```csharp
public static implicit operator T(Const<T> value);
```

- **Description:** Implicitly converts a `Const<T>` back to its underlying value of type `T`.
- **Parameter:** `value` – The `Const<T>` instance to extract the value from.
- **Returns:** The underlying constant value of type `T`.

---

## 🗂 Example of Usage

The example below demonstrates **shared movement speed** across multiple characters using `Const<T>`.

```csharp
//Shared config
public sealed class CharacterConfig : ScriptableObject
{
    [SerializeField] 
    public Const<float> moveSpeed = 5.0f;
}
```

```csharp
//Many instances
public sealed class Character : MonoBehaviour
{
    [SerializeField] 
    private CharacterConfig _config;

    public void MoveStep(Vector3 direction, float deltaTime) 
    {
        this.transform.position += direction * (_config.moveSpeed.Value * deltaTime);
    }
}
```
