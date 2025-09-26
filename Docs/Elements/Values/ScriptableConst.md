# 🧩 ScriptableConst&lt;T&gt;

```csharp
public abstract class ScriptableConst<T> : ScriptableObject, IValue<T>
```

- **Description:** Represents a **serialized, immutable (read-only) constant value** stored as a **ScriptableObject**
  making it perfect for sharing constant values across multiple objects or scenes
- **Inheritance:** `ScriptableObject`, [IValue&lt;T&gt;](IValue.md)
- **Type Parameter:** `T` – The type of the wrapped constant value.
- **Notes:** Supports Unity serialization and Odin Inspector

> [!TIP]  
> Using `ScriptableConst<T>` allows you to change the value in the editor and automatically propagate it to all objects
> that reference it, without changing any code.

---

## 🛠 Inspector Settings

| Parameter            | Description                                                             |
|----------------------|-------------------------------------------------------------------------|
| `value` | The value of this constant |

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
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;](../Functions/IFunction.md)

#### `ToString()`

```csharp
public override string ToString();
```

- **Description:** Returns a string that represents the wrapped constant value.
- **Returns:** A string representation of the constant value.

---

## 🗂 Example of Usage

The example below demonstrates how a speed parameter can be **shared across multiple characters** using
`ScriptableConst`.

```csharp
[CreateAssetMenu(
    fileName = "FloatConst",
    menuName = "Game/Elements/FloatConst"
)]
public sealed class FloatScriptableConst : ScriptableConst<float>
{
}
```

```csharp
public sealed class Character : MonoBehaviour
{
    [SerializeField] 
    private FloatScriptableConst _moveSpeed;

    public void MoveStep(Vector3 direction, float deltaTime) 
    {
        this.transform.position += direction * (_moveSpeed.Invoke() * deltaTime);
    }
}
```
