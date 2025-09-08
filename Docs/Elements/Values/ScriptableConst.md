# 🧩 ScriptableConst&lt;T&gt;

`ScriptableConst<T>` represents a **serialized, immutable (read-only) constant value** stored as a **ScriptableObject**. It implements [IValue&lt;T&gt;](IValue.md) making it perfect for sharing constant values across multiple objects or scenes in `Unity`.

> [!TIP]  
> Using `ScriptableConst<T>` allows you to change the value in the editor and automatically propagate it to all objects that reference it, without changing any code.

---

## Type Parameter

- `T` – The type of the wrapped constant value.

---

## Properties

#### `Value`
```csharp
T Value { get; }
```
- **Description:** Gets the wrapped constant value.
- **Access:** Read-only
---

## Methods
#### `Invoke()`
```csharp
T Invoke()
```
- **Description:** Invokes the function and returns the value.
- **Returns:** The current value of type `T`.
- **Notes**: This is the default implementation from [IFunction&lt;R&gt;](../Functions/IFunction.md#invoke)

#### `ToString()`
```csharp
public override string ToString();
```
- **Description:** Returns a string that represents the wrapped constant value.
- **Returns:** A string representation of the constant value.

---

## 🗂 Example of Usage
The example below demonstrates how a speed parameter can be **shared across multiple characters** using `ScriptableConst`.

```csharp
[CreateAssetMenu(
    fileName = "FloatConst",
    menuName = "Game/Elements/FloatConst"
)]
public sealed class ScriptableFloatConst : ScriptableConst<float>
{
}
```

```csharp
public sealed class Character : MonoBehaviour
{
    [SerializeField] private ScriptableFloatConst _moveSpeed;

    public void MoveStep(Vector3 direction, float deltaTime) 
    {
        this.transform.position += direction * (_moveSpeed.Invoke() * deltaTime);
    }
}
```


