# 🧩 ISetter&lt;T&gt;

Defines a contract for **assigning values**. It extends the [IAction&lt;T&gt;](../Actions/IAction.md#-iactiont)
interface, enabling its usage both as an **action** and as a **value setter**.

```csharp
public interface ISetter<in T> : IAction<T>
```

- **Type Parameter:** `T` – the type of the value to be set.

---

## 🔑 Properties

#### `Value`

```csharp
public T Value { set; }
```

- **Description:** Assigns the provided value.
- **Parameter:** `value` — the new value to be set.

---

## 🏹 Methods

#### `Invoke(T)`

```csharp
public  void Invoke(T arg);
```

- **Description:** Invokes the setter by assigning the provided value.
- **Parameter:** `arg` — the value to set.
- **Notes:** Default implementation comes from [IAction&lt;T&gt;.Invoke()](../Actions/IAction.md#invoket).

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