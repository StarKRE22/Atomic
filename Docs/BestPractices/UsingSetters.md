# 📌 Using Setters with Entities

[ISetter\<T>](../Elements/Setters/Manual.md) provides a **clean interface to set values** on entities. 
This approach allows developers to **decouple input handling, AI, or other logic** from the entity’s internal state, promoting modular and maintainable code.

---

## 📑 Table of Contents

- [Example of Usage](#-example-of-usage)
    - [Create Entity with MoveDirection](#1-create-entity-with-movedirection)
    - [Use ISetter for Movement Input](#2-use-isetter-for-movement-input)
- [Conclusion](#-conclusion)
- [Benefits](#-benefits)

---

## 🗂 Example of Usage

### 1. Create Entity with MoveDirection

```csharp
// Create entity with "MoveDirection" property
var entity = new Entity("Character");
entity.AddValue<ISetter<Vector3>>("MoveDirection", new BaseVariable<Vector3>());
```

---

### 2. Use ISetter for Movement Input

```csharp
// Use "MoveDirection" through the ISetter<Vector3> interface 
public sealed class MoveController : IEntityInit, IEntityTick
{
    private ISetter<Vector3> _moveDirection;

    public void Init(IEntity entity)
    {
        _moveDirection = entity.GetValue<ISetter<Vector3>>("MoveDirection");
    }
    
    public void Tick(IEntity entity, float deltaTime)
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        _moveDirection.Value = new Vector3(dx, 0, dz);
    }
}
```

> [!TIP]
> Using `ISetter<T>` allows any producer (player input, AI, or script) to update the value **without knowing the internal implementation** of the entity.

---

## 🏁 Conclusion

- `ISetter<T>` provides a **standardized interface** for modifying entity values.
- This pattern **decouples input or AI logic** from entity state, improving maintainability.
- Works seamlessly with [Atomic.Entities](../Entities/Manual.md) to handle **movement, stats, or other dynamic properties**.
- Supports **modular, reusable components** that can easily interact with entity properties.
- Encourages **clean and explicit data flow** in gameplay systems.

---

## ✅ Benefits

- Decouples **value producers** from entity state.
- Provides a **clear, type-safe interface** for setting values.
- Simplifies **input handling and AI logic**.
- Supports **modular and maintainable entity components**.
- Encourages **explicit and predictable data flow** in entity systems.  
