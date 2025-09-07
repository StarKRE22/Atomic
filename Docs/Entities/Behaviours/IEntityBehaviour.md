# 🧩️ IEntityBehaviour

`IEntityBehaviour` represents a modular unit of logic that can be attached to an `IEntity`.  
It allows entities to dynamically compose functionality at runtime, following the Entity-State-Behaviour pattern.

---

## Key Features

- **Modular Logic** – Encapsulates a single responsibility or feature of an entity
- **Dynamic Composition** – Can be attached or removed from an entity at runtime
- **Integration with Entity Lifecycle** – Behaviours can respond to Init, Enable, Disable, and Dispose events
- **Lightweight** – Interface only, implementation is left to the developer

---

## Remarks

- Behaviours are typically stateless or encapsulate entity-specific state.
- They can be used to implement features such as movement, attack, game systems, AI, input, or UI controllers.
- `IEntityBehaviour` is intentionally minimal; lifecycle hooks and event handling are defined by optional interfaces (e.g., `IEntityInit`, `IEntityEnable`, `IEntityUpdate`).

## Example Usage

#### 1. Create a character entity
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

#### 2. Write `MoveBehaviour` for the character
```csharp
//Controller that moves entity by its direction
public sealed class MoveBehaviour : IEntityInit, IEntityUpdate
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

    //Calls when Entity.OnUpdate()
    public void OnUpdate(IEntity entity, float deltaTime)
    {
        Vector3 direction = _moveDirection.Value;
        if (direction != Vector3.zero) 
            _position.Value += _moveSpeed.Value * deltaTime * direction;
    }
}
```
#### 3. Add `MoveBehaviour` to the character
```csharp
character.AddBehaviour<MoveBehaviour>();
```
#### 4. **Activate the character when game started**
```csharp
//Make entity active and calls IEntityActivate
character.Activate(); 
```
6. **Update the character while a game is running**
```csharp
const float deltaTime = 0.02f;

while(_isGameRunning)
{
   //Calls IEntityUpdate
   character.OnUpdate(deltaTime); 
}
```