# 🧩 BaseRequest Classes

Base implementations of **IRequest** with 0–4 arguments.  
These classes allow **deferred requests**, which can be invoked and consumed later, preventing duplicate handling.

> **Important:** Unlike regular actions (`IAction`) or events, requests are meant for **deferred execution**.  
> You can call `Invoke` to create a request, and process it later using `Consume`. Repeated `Invoke` calls before `Consume` do not create duplicates.

---

## BaseRequest (no arguments)

```csharp
public class BaseRequest : IRequest
{
    public bool Required { get; }
    public void Invoke();
    public bool Consume();
}
```
- `Invoke()` — marks the request as required.
- `Consume()` — returns `true` if the request was active, and resets the flag.
---
## BaseRequest<T> (one argument)
```csharp
public class BaseRequest<T> : IRequest<T>
{
    public bool Required { get; }
    public T Arg { get; }
    public void Invoke(T arg);
    public bool TryGet(out T arg);
    public bool Consume(out T arg);
}
```
- `Arg` — stores the argument value.
- `Invoke(arg)` — marks the request and stores the argument.
- `TryGet(out arg)` — retrieves the argument without resetting the flag.
- `Consume(out arg)` — retrieves the argument and resets the request.
---
## BaseRequest<T1, T2> (two arguments)
```csharp
public class BaseRequest<T1, T2> : IRequest<T1, T2>
{
    public bool Required { get; }
    public T1 Arg1 { get; }
    public T2 Arg2 { get; }
    public void Invoke(T1 arg1, T2 arg2);
    public bool TryGet(out T1 arg1, out T2 arg2);
    public bool Consume(out T1 arg1, out T2 arg2);
}
```
- Allows storing and processing two deferred arguments.
---
## BaseRequest<T1, T2, T3> (three arguments)
```csharp
public class BaseRequest<T1, T2, T3> : IRequest<T1, T2, T3>
{
    public bool Required { get; }
    public T1 Arg1 { get; }
    public T2 Arg2 { get; }
    public T3 Arg3 { get; }
    public void Invoke(T1 arg1, T2 arg2, T3 arg3);
    public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3);
    public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3);
}
```
- Used for deferred storage and processing of three arguments.
---
## BaseRequest<T1, T2, T3, T4> (four arguments)
```csharp
public class BaseRequest<T1, T2, T3, T4> : IRequest<T1, T2, T3, T4>
{
    public bool Required { get; }
    public T1 Arg1 { get; }
    public T2 Arg2 { get; }
    public T3 Arg3 { get; }
    public T4 Arg4 { get; }
    public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
    public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
    public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
}
```
- Supports deferred storage and processing of four arguments.
---

## Deferred Request Example

This example demonstrates how a request can be **triggered** by a **MoveController** and **consumed** later by **MoveBehaviour** in `FixedUpdate`.
It allows deferred execution, prevents duplicate handling, and controls the timing of request processing.

### 1. Create the input controller
```csharp
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

        // Trigger the movement request
        // Repeated calls to Invoke before Consume do not create duplicates
        _moveRequest.Invoke(desiredMove);
    }
}
```
---
### 2. Create the move behaviour
```csharp
public sealed class MoveBehaviour
{
    private readonly Transform _transform;
    private readonly IValue<float> _moveSpeed;
    private readonly IRequest<Vector3> _moveRequest; // Same request instance
    
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
        // Consume the movement request
        if (_moveRequest.Consume(out Vector3 moveDirection))
        {
            // Process the movement
            _transform.position += moveDirection * Time.fixedDeltaTime * _moveSpeed.Value;
        }
    }
}
```
---
### 3. Create the move request implementation
```csharp
var moveRequest = new BaseRequest<Vector3>();
var moveController = new MoveController(moveRequest);
var moveBehaviour = new MoveBehaviour(transform, moveSpeed, moveRequest);
```
---
### 4. Process controller and behaviour
```csharp
void Update()
{
    moveController.Update();
}

void FixedUpdate()
{
    moveBehaviour.FixedUpdate();
}
```
