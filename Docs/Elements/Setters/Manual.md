# 🧩 Setters

Provides interfaces and classes for **assigning values**. It offers a lightweight and consistent way to encapsulate
value assignment logic, enabling clean, reusable, and decoupled code for modifying data.

- [ISetter](ISetter.md) <!-- + -->
- [InlineSetter](InlineSetter.md) <!-- + -->

---

## 🗂 Example of Usage

Below is an example of using `ISetter<Vector3>` inside a movement input controller built with `Atomic.Entities`. This
approach cleanly separates **input handling** from the **entity’s movement logic**, while relying only on the `ISetter`
interface.

```csharp
//Create entity with "MoveDirection" property
var entity = new Entity("Character");
entity.AddValue<ISetter<Vector3>>("MoveDirection", new BaseVariable<Vector3>());
```

```csharp
//Use "MoveDirection" through the ISetter<Vector3> interface 
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