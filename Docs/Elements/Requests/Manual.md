# 🧩 Requests

Represents **deferred actions** that can be executed at a later time. It is particularly useful for scenarios where
input is collected in one phase (e.g., `Update`) but processed in another (e.g., `FixedUpdate`).
Requests also help **prevent duplicate commands** by ensuring the same request is not processed multiple times while
active.

- [IRequests](IRequests.md) <!-- + -->
    - [IRequest](IRequest.md) <!-- + -->
    - [IRequest&lt;T&gt;](IRequest%601.md) <!-- + -->
    - [IRequest&lt;T1, T2&gt;](IRequest%602.md) <!-- + -->
    - [IRequest&lt;T1, T2, T3&gt;](IRequest%603.md)  <!-- + -->
    - [IRequest&lt;T1, T2, T3, T4&gt;](IRequest%604.md) <!-- + -->
- [BaseRequests](BaseRequests.md) <!-- + -->
    - [BaseRequest](BaseRequest.md) <!-- + -->
    - [BaseRequest&lt;T&gt;](BaseRequest%601.md) <!-- + -->
    - [BaseRequest&lt;T1, T2&gt;](BaseRequest%602.md) <!-- + -->
    - [BaseRequest&lt;T1, T2, T3&gt;](BaseRequest%603.md) <!-- + -->
    - [BaseRequest&lt;T1, T2, T3, T4&gt;](BaseRequest%604.md) <!-- + -->

---

## 🗂 Examples of Usage

Below are examples of using request with the `Atomic.Entities` framework.

---

### Example #1: Move Input Using Requests

This example demonstrates how a `MoveController` can **produce a request in update**, and `MoveBehaviour` can **consume
it later in fixed update**:

```csharp
//Add to entity "MoveRequest" as "BaseRequest<Vector3>"
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
        Vector3 moveDirection = new Vector3(dx, 0, dz );
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

### Example #2: Target Following Using Requests

In this example, a `AIFollowBehaviour` triggers a movement request, which is later processed by `MoveBehaviour`:

```csharp
//Add to entity "MoveRequest" as "BaseRequest<Vector3>"
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

## 📝 Notes

- **Deferred execution** – Requests can be stored and processed later via `Consume()`.
- **Duplicate prevention** – Multiple identical requests can be avoided because `Consume()` only processes requests that
  are still required.
- **Required** – Indicates whether the request currently needs handling.
- **TryGet / Consume** – Methods to safely inspect or process the request arguments.