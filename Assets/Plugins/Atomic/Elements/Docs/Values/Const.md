
## 🧩 Const<T>

`Const<T>` represents a **serialized, immutable (read-only) constant value wrapper**.  
It implements `IValue<T>` and supports **implicit conversions**, making it useful in systems where values must be serialized or treated as data sources.

> **Note:** Unlike regular value types, `Const<T>` is a **reference type**, making it lightweight to pass around.  
> It can act as a **lightweight pattern**, for example, to share a constant value across multiple systems without copying the value.

---

#### Type Parameter

- `T` – The type of the wrapped constant value.

---

#### Constructors

```csharp
// Default constructor
public Const()

// Constructor with a specified value
public Const(T value)
```
- Description:
  - Const() initializes a new instance with the default value of T.
  - Const(T value) initializes a new instance with a specified constant value.

#### Properties
```csharp
T Value { get; }
```
- Description: Gets the wrapped constant value.
- Access: Read-only
#### Methods
```csharp
T Invoke()
```

### Example of Usage
```csharp
public sealed class Character : MonoBehaviour
{
    private IValue<float> _moveSpeed;
    
    public void SetMoveSpeed(IValue<float> moveSpeed) =>
        _moveSpeed = moveSpeed;
    
    public void MoveStep(Vector3 direction) =>
        this.transform.position += direction * moveSpeed;
    
}

public sealed class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private Const<float> _moveSpeed = 3f;  // Shared constant for all characters
    
    [SerializeField]
    private Character[] _characters; // 100+ characters
    
    void Start()
    {
        // Assign the same Const<float> instance to all characters
        foreach (Character character in _characters)
            character.SetMoveSpeed(_moveSpeed);
    }
}
```