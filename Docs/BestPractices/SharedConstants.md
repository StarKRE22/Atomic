#  📌 Using Flyweight Pattern for Constants

Whenever you have a **read-only, constant value** that multiple objects need to access, wrap it in [Const\<T\>](../Elements/Values/Const.md).  

This approach makes your code **lightweight, safe, and reference-friendly**, avoiding unnecessary copies of data.

---

## ✅ Benefits
- Acts as a **lightweight reference type**, even for primitive or value types.
- Can be **shared across multiple objects or systems** without duplication.
- Implements [IValue<T>](../Elements/Values/IValue.md), making the project **more maintainable and testable**.
- Keeps data **immutable**, preventing accidental modifications.

---

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

---

## 🔹 Default Constants

`Const<T>` provides a convenient way to represent **default constant values** that can be **reused across your project**.  
These constants are **lightweight**, **immutable**, and can be **safely shared** between multiple objects or components.

> [!NOTE] 
> You can explore all available default constants by following this link: [DefaultConstants](../Elements/Values/DefaultConstants.md)

---