# 🧩 Entity Behaviours

**Entity Behaviour** is a modular controller that handles [IEntity](../Entities/IEntity.md) lifecycle events and
attached to concrete instance of entity. It allows
entities to dynamically compose functionality at runtime, following the **Entity-State-Behaviour** pattern.

There are separate interfaces that handles the corresponding lifecycle events of the entity.

- [IEntityBehaviour](IEntityBehaviour.md) <!-- + -->
- [IEntityInit](IEntityInit.md) <!-- + -->
- [IEntityInit&lt;E&gt;](IEntityInit%601.md) <!-- + -->
- [IEntityDispose](IEntityDispose.md) <!-- + -->
- [IEntityDispose&lt;E&gt;](IEntityDispose%601.md) <!-- + -->
- [IEntityEnable](IEntityEnable.md) <!-- + -->
- [IEntityEnable&lt;E&gt;](IEntityEnable%601.md) <!-- + -->
- [IEntityDisable](IEntityDisable.md) <!-- + -->
- [IEntityDisable&lt;E&gt;](IEntityDisable%601.md) <!-- + -->
- [IEntityTick](IEntityTick.md) <!-- + -->
- [IEntityTick&lt;E&gt;](IEntityTick%601.md) <!-- + -->
- [IEntityFixedTick](IEntityFixedTick.md) <!-- + -->
- [IEntityFixedTick&lt;E&gt;](IEntityFixedTick%601.md) <!-- + -->
- [IEntityLateTick](IEntityLateTick.md) <!-- + -->
- [IEntityLateTick&lt;E&gt;](IEntityLateTick%601.md) <!-- + -->
- [IEntityGizmos](IEntityGizmos.md) <!-- + -->
- [IEntityGizmos&lt;E&gt;](IEntityGizmos%601.md) <!-- + -->

---

## 🗂 Example Usage

Below is an example of using `MoveBehaviour` for the entity:

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