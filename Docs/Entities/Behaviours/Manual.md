# 🧩 Behaviours

**Behaviour** is a modular unit of logic that can be attached to an [IEntity](../Entities/IEntity.md).  
It allows entities to dynamically compose functionality at runtime, following the **Entity-State-Behaviour** pattern.

Each behaviour can handle different events of the entity:

| Event       | Purpose                                                    |
|-------------|------------------------------------------------------------|
| `Init`      | Initialization of the behaviour when the entity is created |
| `Enable`    | Activating the entity on the scene or in a pool            |
| `Disable`   | Deactivating the entity and returning it to the pool       |
| `Tick`      | Updates every frame (logic, state)                         |
| `FixedTick` | Physics and game mechanics updates with a fixed timestep   |
| `LateTick`  | Updates after rendering (e.g., UI)                         |
| `Dispose`   | Releasing entity resources when it is destroyed            |

Each phase has a separate interface that handles the corresponding lifecycle stage of the entity.

---

## 🔍 API Reference

For each event, there is a dedicated interface that represents that phase:

- **Init**
    - [IEntityInit](IEntityInit.md) <!-- + -->
    - [IEntityInit&lt;E&gt;](IEntityInit%601.md) <!-- + -->
- **Dispose**
    - [IEntityDispose](IEntityDispose.md) <!-- + -->
    - [IEntityDispose&lt;E&gt;](IEntityDispose%601.md) <!-- + -->
- **Enable**
    - [IEntityEnable](IEntityEnable.md) <!-- + -->
    - [IEntityEnable&lt;E&gt;](IEntityEnable%601.md) <!-- + -->
- **Disable**
    - [IEntityDisable](IEntityDisable.md)
    - [IEntityDisable&lt;E&gt;](IEntityDisable%601.md)
- **Tick**
    - [IEntityTick](IEntityTick.md)
    - [IEntityTick&lt;E&gt;](IEntityTick%601.md)
- **FixedTick**
    - [IEntityFixedTick](IEntityFixedTick.md)
    - [IEntityFixedTick&lt;E&gt;](IEntityFixedTick%601.md)
- **LateTick**
    - [IEntityLateTick](IEntityLateTick.md)
    - [IEntityLateTick&lt;E&gt;](IEntityLateTick%601.md)
- **Gizmos**
    - [IEntityGizmos](IEntityGizmos.md)
    - [IEntityGizmos&lt;E&gt;](IEntityGizmos%601.md) 
- [IEntityBehaviour](IEntityBehaviour.md) <!-- + -->

> This approach makes entities flexible and extensible, allowing new behaviours to be added easily without modifying the
> core code.

---

## 🗂 Example Usage

#### 1. Create a new instance of entity

```csharp
//Create a new entity
var character = new Entity("Character");

//Add tags
character.AddTag("Moveable");

//Add properties
character.AddValue("Position", new ReactiveVariable<Vector3>());
character.AddValue("MoveSpeed", new Const<float>(3.5f));
character.AddValue("MoveDirection", new ReactiveVariable<Vector3>());
```

#### 2. Create `MoveBehaviour` for the entity

```csharp
//Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityFixedTick
{
    private IVariable<Vector3> _position;
    private IValue<float> _moveSpeed;
    private IValue<Vector3> _moveDirection;

    //Calls when Entity.Init()
    public void Init(IEntity entity)
    {
        _position = entity.GetValue<IVariable<Vector3>>("Position");
        _moveSpeed = entity.GetValue<IValue<float>>("MoveSpeed");
        _moveDirection = entity.GetValue<IValue<Vector3>>("MoveDirection");
    }

    //Calls when Entity.FixedTick()
    public void FixedTick(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _position.Value += _moveSpeed.Value * deltaTime * direction;
    }
}
```

#### 3. Attach `MoveBehaviour` to the entity

```csharp
//Add behaviour
character.AddBehaviour<MoveBehaviour>();
```

---

## 📝 Notes

- **Modular Logic** – Encapsulates a single responsibility or feature of an entity
- **Dynamic Composition** – Can be attached or removed from an entity at runtime
- **Integration with Entity Lifecycle** – Behaviours can respond to Init, Enable, Disable, and Dispose events
- **Lightweight** – Interface only, implementation is left to the developer
- Behaviours are typically stateless or encapsulate entity-specific state.
- They can be used to implement features such as movement, attack, game systems, AI, input, or UI controllers.