# 🧩 Const

`Const<T>` represents a **serialized, immutable (read-only) constant value wrapper**. It implements [IValue&lt;T&gt;](IValue.md) and supports **implicit conversions**, making it useful in systems where values must be serialized or treated as data sources.

> [!NOTE]
> Unlike regular value types, `Const<T>` is a **reference type**, making it lightweight to pass around. It can act as a **lightweight pattern**, for example, to share a constant value across multiple systems without copying the value.

---

## Type Parameter

- `T` – The type of the wrapped constant value.

---

## Constructors

```csharp
// Default constructor
public Const()

// Constructor with a specified value
public Const(T value)
```
- Description:
  - Const() initializes a new instance with the default value of T.
  - Const(T value) initializes a new instance with a specified constant value.

## Properties
```csharp
T Value { get; }
```
- Description: Gets the wrapped constant value.
- Access: Read-only
## Methods
```csharp
T Invoke()
```
## 🗂 Example of Usage

The example below demonstrates **shared movement speed** across multiple characters using `Const<T>`.

```csharp
public sealed class CharacterConfig : ScriptableObject
{
    [SerializeField] public Const<float> moveSpeed = 5;
}

public sealed class Character : MonoBehaviour
{
    [SerializeField] private CharacterConfig _config;

    public void MoveStep(Vector3 direction, float deltaTime) 
    {
        this.transform.position += direction * (_config.moveSpeed.Value * deltaTime);
    }
}
```