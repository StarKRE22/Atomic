# 🧩 ScriptableConst

`ScriptableConst<T>` represents a **serialized, immutable (read-only) constant value** stored as a **ScriptableObject**.  
It implements `IValue<T>` making it perfect for sharing constant values across multiple objects or scenes in Unity.

> [!TIP]  
> Using `ScriptableConst<T>` allows you to change the value in the editor and automatically propagate it to all objects that reference it, without changing any code.

---

## Type Parameter

- `T` – The type of the wrapped constant value.

---

## Properties
```csharp
T Value { get; }
```
- **Description:** Gets the wrapped constant value.
- **Access:** Read-only

---

## Methods
```csharp
T Invoke()
```
- **Description:** Returns the constant value (implements `IValue<T>`).
- **Usage:** Allows `ScriptableConst<T>` to be used interchangeably with other `IValue<T>` objects.

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

public sealed class Character : MonoBehaviour
{
    [SerializeField] private ScriptableFloatConst _moveSpeed;

    public void MoveStep(Vector3 direction, float deltaTime) 
    {
        this.transform.position += direction * (_moveSpeed.Invoke() * deltaTime);
    }
}
```


