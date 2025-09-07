# 📌 Requests vs Actions

When designing game mechanics, you may often wonder **what the difference is between `Requests` and `Actions`**, since both perform operations. Which one should you choose in a given situation?

---

## 🏹 Actions

An **Action** executes immediately. For example, if a character triggers a shooting action or interact with pick up, it is performed instantly:

### Shooting weapon
```csharp
IEntity character = ...
IAction fireAction = character.GetFireAction();
fireAction.Invoke(); // Executes immediately!
```

### Interacting with item
```csharp
IEntity character = ...
IEntity pickUp = ...

IAction<IEntity> interactAction = pickUp.GetInteractAction();
interactAction.Invoke(character); // Executes immediately!
```

---

## ⏳ Requests

A **Request** has a slightly different nature: it represents a deferred action that can be executed later. This is particularly useful when **player input occurs in `Update`**, but the request is processed in `FixedUpdate`. Requests also prevent **duplicate commands** if the same request is already active.

###  Move Input Using Requests

```csharp
public sealed class MoveController : IEntityInit, IEntityUpdate
{
    private readonly IRequest<Vector3> _moveRequest;
    
    public void Init(IEntity entity)
    {
        _moveRequest = entity.GetMoveRequest();    
    }

    public void OnUpdate(IEntity entity, float deltaTime)
    {
        Vector3 desiredMove = new Vector3(
            Input.GetAxis("Horizontal"), 
            0,
            Input.GetAxis("Vertical")
        );

        _moveRequest.Invoke(desiredMove);
    }
}

// MoveBehaviour consumes the request
public sealed class MoveBehaviour
{
private readonly Transform _transform;
private readonly IValue<float> _moveSpeed;
private readonly IRequest<Vector3> _moveRequest;

    public MoveBehaviour(
        Transform transform,
        IValue<float> moveSpeed,
        IRequest<Vector3> moveRequest
    )
    {
        _transform = transform;
        _moveSpeed = moveSpeed;
        _moveRequest = moveRequest;
    }

    public void FixedUpdate()
    {
        if (_moveRequest.Consume(out Vector3 moveDirection))
        {
            _transform.position += moveDirection * Time.fixedDeltaTime * _moveSpeed.Value;
        }
    }
}
```



For AI behavior, Requests are also very useful:

!!!

// Example: Player Input
// (pseudo-code)
Update() {
_jumpRequest.Invoke();
}
FixedUpdate() {
if (_jumpRequest.Consume()) { /* perform jump */ }
}
!!!

!!!

// Example: AI Input
// (pseudo-code)
Update() {
_moveRequest.Invoke(targetPosition);
}
FixedUpdate() {
if (_moveRequest.Consume(out Vector3 direction)) { /* move AI */ }
}
!!!

---

## Choosing Between Requests and Actions

- **Single-player games**  
  Use **Requests** in systems like `InputControllers` or AI.  
  Requests allow **deferred execution**, ensuring actions are handled cleanly in the next frame without duplication.

- **Multiplayer/networked games**  
  Use **Actions** to propagate events and commands across clients.  
  Actions are better for **decoupling and broadcasting behavior**.

---

### Special Considerations for Multiplayer

In multiplayer games, **Actions are generally preferred over Requests** because:

- The client's **tick-rate is synchronized and re-simulated**, making immediate execution more reliable.
- Using Requests would require **additional network synchronization** for request flags, which adds unnecessary complexity.

---

## 🕹 Example: Single-player Input Using Requests

!!!


!!!
