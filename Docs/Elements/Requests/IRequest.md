# 🧩 IRequest

These interfaces represent **request actions** that may carry from zero to four typed arguments.  
Unlike standard `IAction` or event handlers, **requests are designed to represent actions that can be deferred**.  
This allows the system to **queue, delay, or coalesce duplicate requests**, processing them later through `Consume()`
without immediately executing the logic.

---

## Key Notes

- **Deferred execution** – Requests can be stored and processed later via `Consume()`.
- **Duplicate prevention** – Multiple identical requests can be avoided because `Consume()` only processes requests that
  are still required.
- **Required** – Indicates whether the request currently needs handling.
- **TryGet / Consume** – Methods to safely inspect or process the request arguments.

---

## IRequest

A **parameterless request**.

```csharp
public interface IRequest : IAction
{
    bool Required { get; }
    bool Consume();
}
```

- **Required** – Indicates if the request needs handling.
- **Consume()** – Attempts to consume the request. If consumed, the request is no longer required.

---

## IRequest&lt;T&gt;

A **single-argument request**.

```csharp
public interface IRequest<T> : IAction<T>
{
    bool Required { get; }
    T Arg { get; }
    bool TryGet(out T arg);
    bool Consume(out T arg);
}
```

- **T** – Type of the argument.
- **Arg** – The request argument.
- **TryGet(out T arg)** – Tries to retrieve the argument without consuming the request.
- **Consume(out T arg)** – Consumes the request and retrieves the argument. After this, the request is marked as
  handled.

---

## IRequest<T1, T2>

A **two-argument request**.

```csharp
public interface IRequest<T1, T2> : IAction<T1, T2>
{
    bool Required { get; }
    T1 Arg1 { get; }
    T2 Arg2 { get; }
    bool TryGet(out T1 args1, out T2 args2);
    bool Consume(out T1 args1, out T2 args2);
}
```

- **T1**, **T2** – Types of the arguments.
- **Arg1**, **Arg2** – The request arguments.
- **TryGet(out T1 arg1, out T2 arg2)** – Tries to retrieve both arguments without consuming the request.
- **Consume(out T1 arg1, out T2 arg2)** – Consumes the request and retrieves both arguments. Prevents duplicate
  handling.

---

## IRequest<T1, T2, T3>

```csharp
public interface IRequest<T1, T2, T3> : IAction<T1, T2, T3>
{
    bool Required { get; }
    T1 Arg1 { get; }
    T2 Arg2 { get; }
    T3 Arg3 { get; }
    bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3);
    bool Consume(out T1 arg1, out T2 arg2, out T3 arg3);
}
```

A **three-argument request**.

- **T1**, **T2**, **T3** – Types of the arguments.
- **Arg1**, **Arg2**, **Arg3** – The request arguments.
- **TryGet(out T1 arg1, out T2 arg2, out T3 arg3)** – Tries to retrieve all three arguments without consuming the
  request.
- **Consume(out T1 arg1, out T2 arg2, out T3 arg3)** – Consumes the request and retrieves all three arguments. Ensures
  deferred execution without duplication.

---

## IRequest<T1, T2, T3, T4>

A **four-argument request**.

```csharp
public interface IRequest<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
{
    bool Required { get; }
    T1 Arg1 { get; }
    T2 Arg2 { get; }
    T3 Arg3 { get; }
    T4 Arg4 { get; }
    bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
    bool Consume(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
}
```

- **T1**, **T2**, **T3**, **T4** – Types of the arguments.
- **Arg1**, **Arg2**, **Arg3**, **Arg4** – The request arguments.
- **TryGet(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4)** – Tries to retrieve all four arguments without
  consuming the request.
- **Consume(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4)** – Consumes the request and retrieves all four
  arguments. Useful for deferred and deduplicated processing.

## Deferred Request Example

This example shows how a request can be **triggered by a MoveController** and **consumed later by the MoveBehaviour** in
`FixedUpdate`, ensuring deferred execution and preventing duplicate handling.

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
        // Repeated calls before Consume do not duplicate the request
        _moveRequest.Invoke(desiredMove);
    }
}

public sealed class MoveBehaviour
{
    private readonly Transform _transform;
    private readonly IValue<float> _moveSpeed;
    private readonly IRequest<Vector3> _moveRequest; //Same request instance
    
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