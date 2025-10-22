# 📌 Requests vs Actions

When designing game mechanics, you might wonder **what the difference is
between [Requests](../Elements/Requests/Manual.md) and [Actions](../Elements/Actions/Manual.md)**, since both
perform operations.
This section explains when to use each and how they behave in practice.

---

## 📑 Table of Contents

- [Actions](#-actions)
- [Requests](#-requests)
- [Multiplayer Considerations](#-multiplayer-considerations)
- [Conclusion](#-conclusion)

---

## 🏹 Actions

An **Action** executes **immediately**.  
For example, if a character fires a weapon or interacts with a pick-up, it happens instantly.

### 1. Shooting a weapon

```csharp
IEntity character = ...
IAction fireAction = character.GetFireAction();
fireAction.Invoke(); // Executes immediately!
```

### 2. Interacting with an item

```csharp
IEntity character = ...
IEntity pickUp = ...

IAction<IEntity> interactAction = pickUp.GetInteractAction();
interactAction.Invoke(character); // Executes immediately!
```

---

## ⏳ Requests

A **Request** represents a **deferred action** that can be executed later.  
This is useful when:

- Player input happens in `Update`, but execution occurs in `FixedUpdate`.
- You want to **prevent duplicate commands** if the same request is already active.

### 1. Move Input Using Requests

**MoveController** produces a request in `Tick`, and **MoveBehaviour** consumes it in `FixedTick`:

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

An AI system can produce a movement request, which is later processed by `MoveBehaviour`:

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

// Same MoveBehaviour consumes the request (same as above)
```

---

## 🕹 Multiplayer Considerations

In multiplayer games:

- **Actions** are generally preferred because client tick-rates are synchronized and re-simulated, making immediate
  execution reliable.
- Using **Requests** would require additional network synchronization for request flags, adding complexity.

---

## 🏁 Conclusion

**Choose Requests for deferred logic, Actions for immediate execution.**

- **Single-player games**  
  Use **Requests** for systems like `InputControllers` or AI.  
  They allow deferred execution, preventing duplicates and keeping input handling clean.


- **Multiplayer / networked games**  
  Use **Actions** to propagate events across clients.  
  Actions are better for broadcasting behavior and decoupling systems.
