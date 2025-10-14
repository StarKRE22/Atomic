
## 🗂 Examples of Usage

Below are examples of using request with the `Atomic.Entities` framework.

---

### 1️⃣ Move Input Using Requests

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

### 2️⃣ Target Following Using Requests

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