## 🏆 Requests vs Actions

When designing game systems, the choice between **Requests** and **Actions** depends on context:

- **Single-player games**  
  Use **Requests** in components like `InputControllers` or AI systems.  
  Requests allow **deferred execution**, ensuring input or AI decisions are processed cleanly in the next frame without duplication.

- **Multiplayer/networked games**  
  Use **Actions** for propagating events and commands.  
  Actions are better suited for decoupling and broadcasting behavior across multiple clients or systems.

---

### Example: Single-player input using Requests

```csharp
// MoveController triggers a request
public sealed class MoveController 
{
    private readonly IRequest<Vector3> _moveRequest;

    public MoveController(IRequest<Vector3> moveRequest)
    {
        _moveRequest = moveRequest;
    }

    public void Update()
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