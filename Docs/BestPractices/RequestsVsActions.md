# 📌 Requests vs Actions

When designing game mechanics, you may often wonder **what the difference is between `Requests` and `Actions`**, since both perform operations. Which one should you choose in a given situation?

---

## 🏹 Actions

An **Action** executes immediately. For example, if a character triggers a shooting action or interact with pick up, it is performed instantly:

### 1. Shooting weapon
```csharp
IEntity character = ...
IAction fireAction = character.GetFireAction();
fireAction.Invoke(); // Executes immediately!
```

### 2. Interacting with item
```csharp
IEntity character = ...
IEntity pickUp = ...

IAction<IEntity> interactAction = pickUp.GetInteractAction();
interactAction.Invoke(character); // Executes immediately!
```

---

## ⏳ Requests

A **Request** has a slightly different nature: it represents a deferred action that can be executed later. This is particularly useful when **player input occurs in `Update`**, but the request is processed in `FixedUpdate`. Requests also prevent **duplicate commands** if the same request is already active.

### 1. Move Input Using Requests
This example demonstrates how a `MoveController` can **produce a request in update**, and `MoveBehaviour` can **consume it later in fixed update**:

```csharp
// MoveController produces the request
public sealed class MoveController : IEntityInit, IEntityTick
{
    private IRequest<Vector3> _moveRequest;
    
    public void Init(IEntity entity)
    {
        _moveRequest = entity.GetMoveRequest();    
    }

    public void Tick(IEntity entity, float deltaTime)
    {
        float dx = Input.GetAxis("Horizontal");
        float dz = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(dx, 0, dz );
        _moveRequest.Invoke(moveDirection);
    }
}

// MoveBehaviour consumes the request
public sealed class MoveBehaviour : IEntityInit, IEntityFixedTick
{
    private Transform _transform;
    private IValue<float> _moveSpeed;
    private IRequest<Vector3> _moveRequest;

    public void Init(IEntity entity)
    {
        _transform = entity.GetTransform();
        _moveSpeed = entity.GetMoveSpeed();
        _moveRequest = entity.GetMoveRequest();
    }

    public void FixedTick(IEntity entity, float deltaTime)
    {
        if (_moveRequest.Consume(out Vector3 moveDirection))
            _transform.position += moveDirection * Time.fixedDeltaTime * _moveSpeed.Value;
    }
}
```

### 2. Target Following Using Requests
In this example, a `AIFollowBehaviour` triggers a movement request, which is later processed by `MoveBehaviour`:

```csharp
// AIFollowBehaviour produces the request
public sealed class AIFollowBehaviour : IEntityInit, IEntityTick
{
    private IValue<IEntity> _target;
    private IValue<Vector3> _position;
    private IRequest<Vector3> _moveRequest;
    private IValue<float> _stoppingDistance;
    
    public void Init(IEntity entity)
    {
        _target = entity.GetTarget();
        _position = entity.GetPosition();
        _moveRequest = entity.GetMoveRequest();    
        _stoppingDistance = entity.GetStoppingDistance();
    }

    public void Tick(IEntity entity, float deltaTime)
    {
        IEntity target = _target.Value;
        if (target == null)
            return;
        
        Vector3 delta = _target.GetPosition().Value - _position.Value;
        if (delta.magnitude <= _stoppingDistance.Value)
            return;
        
        Vector3 moveDirection = delta.normalized;
        _moveRequest.Invoke(moveDirection);
    }
}

// MoveBehaviour consumes the request
public sealed class MoveBehaviour : IEntityInit, IEntityFixedTick
{
    //Same code 
}
```

---

## 🕹 About Multiplayer

In multiplayer games, **Actions are generally preferred over Requests** because:

- The client's **tick-rate is synchronized and re-simulated**, making immediate execution more reliable.
- Using Requests would require **additional network synchronization** for request flags, which adds unnecessary complexity.

---


## 🏁 Conclusion
**Choose Requests for deferred logic, Actions for immediate execution.**

- **Single-player games**  
  Use **Requests** in systems like `InputControllers` or `AI`.  
  Requests allow **deferred execution**, ensuring actions are handled cleanly in the next frame without duplication.


- **Multiplayer / networked games**  
  Use **Actions** to propagate events and commands across clients.  
  Actions are better for **decoupling and broadcasting behavior**.

---