# 📌 Using Requests with Entities

[Requests](../Elements/Requests/Manual.md) provide a **flexible way to decouple producers and consumers** in your entity
logic.
Producers can create requests (e.g., player input or AI decisions), and consumers can process them later, typically in a
different update loop.

---

## 📑 Table of Contents

- [Examples of Usage](#-examples-of-usage)
    - [Move Input Using Requests](#1-move-input-using-requests)
    - [Target Following Using Requests](#2-target-following-using-requests)
- [Conclusion](#-conclusion)
- [Benefits](#-benefits)

---

## 🗂 Examples of Usage

### 1️⃣ Move Input Using Requests

This example demonstrates how a `MoveController` **produces a request in Update**, and `MoveBehaviour` **consumes it in
FixedUpdate**:

```csharp
// Add to entity "MoveRequest" as "BaseRequest<Vector3>"
entity.AddMoveRequest(new BaseRequest<Vector3>());
entity.AddBehaviour<MoveController>();
entity.AddBehaviour<MoveBehaviour>();
```

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
        Vector3 moveDirection = new Vector3(dx, 0, dz);
        _moveRequest.Invoke(moveDirection);
    }
}
```

```csharp
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

    public void FixedTick((IEntity entity, float deltaTime))
    {
        if (_moveRequest.Consume(out Vector3 moveDirection))
            _transform.position += moveDirection * Time.fixedDeltaTime * _moveSpeed.Value;
    }
}
```

---

### 2️⃣ Target Following Using Requests

A `AIFollowBehaviour` can produce movement requests for AI-controlled entities, which are later consumed by
`MoveBehaviour`:

```csharp
// Add to entity "MoveRequest" as "BaseRequest<Vector3>"
entity.AddMoveRequest(new BaseRequest<Vector3>());
entity.AddBehaviour<AIFollowBehaviour>();
entity.AddBehaviour<MoveBehaviour>();
```

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
```

```csharp
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

    public void FixedTick((IEntity entity, float deltaTime))
    {
        if (_moveRequest.Consume(out Vector3 moveDirection))
            _transform.position += moveDirection * Time.fixedDeltaTime * _moveSpeed.Value;
    }
}
```

---

## 🏁 Conclusion

- [Requests](../Elements/Requests/Manual.md) allow **decoupling producers and consumers** within entity
  systems.
- Producers (like `MoveController` or `AIFollowBehaviour`) can generate requests independently of when consumers process
  them.
- Consumers (like `MoveBehaviour`) can safely process requests in **FixedUpdate** or another context, ensuring
  predictable and modular behavior.
- This pattern supports **clean, reactive gameplay logic** and improves separation of responsibilities.
- Integrates seamlessly with the [Atomic.Entities](../Entities/Manual.md) framework, enabling **player input, AI
  decisions, and other dynamic interactions**.

---

## ✅ Benefits

- Decouples **input/decision logic** from execution logic.
- Ensures **modular and maintainable entity behaviors**.
- Enables **safe multi-stage processing** (Update → FixedUpdate).
- Supports **both player-controlled and AI-controlled entities**.
- Reduces boilerplate and improves clarity in **request-driven systems**.  