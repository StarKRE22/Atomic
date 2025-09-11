# 🧩 BaseRequest Classes

The **BaseRequest** classes provide **concrete implementations** of the [IRequest](IRequest.md) interfaces. They are designed to store request state and optionally one to four arguments. These classes **track whether a request is required** and allow **deferred consumption**.

> [!IMPORTANT]
> Unlike regular actions, requests are meant for **deferred execution**. You can call `Invoke` to create a request, and process it later using `Consume`. Repeated `Invoke` calls before `Consume` do not create duplicates.


---

## 🧩 BaseRequest

!!!csharp
public class BaseRequest : IRequest
!!!
- **Description:** Represents a basic request without parameters.

### Properties

#### `Required`
!!!csharp
public bool Required { get; }
!!!
- **Description:** Indicates whether the request is currently required.

### Methods

#### `Invoke()`
!!!csharp
public void Invoke();
!!!
- **Description:** Marks the request as required.

#### `Consume()`
!!!csharp
public bool Consume();
!!!
- **Description:** Attempts to consume the request.
- **Returns:** `true` if the request was required and is now consumed.

---

## 🧩 BaseRequest<T>

!!!csharp
public class BaseRequest<T> : IRequest<T>
!!!
- **Description:** Represents a request with a single argument.
- **Type parameter:** `T` — type of the argument.

### Properties

#### `Required`
!!!csharp
public bool Required { get; }
!!!
- **Description:** Indicates whether the request is currently required.

#### `Arg`
!!!csharp
public T Arg { get; }
!!!
- **Description:** The stored argument.

### Methods

#### `Invoke(T)`
!!!csharp
public void Invoke(T arg);
!!!
- **Description:** Marks the request as required and stores the argument.
- **Parameter:** `arg` — the argument value.

#### `Consume(out T arg)`
!!!csharp
public bool Consume(out T arg);
!!!
- **Description:** Attempts to consume the request and retrieve the argument.
- **Output parameter:** `arg` — the argument if successfully consumed.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T arg)`
!!!csharp
public bool TryGet(out T arg);
!!!
- **Description:** Attempts to retrieve the argument without consuming the request.
- **Output parameter:** `arg` — the stored argument.
- **Returns:** `true` if the request is currently required.

---

## 🧩 BaseRequest<T1, T2>

!!!csharp
public class BaseRequest<T1, T2> : IRequest<T1, T2>
!!!
- **Description:** Represents a request with two arguments.
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument

### Properties

#### `Required`
!!!csharp
public bool Required { get; }
!!!
- **Description:** Indicates whether the request is currently required.

#### `Arg1`
!!!csharp
public T1 Arg1 { get; }
!!!
- **Description:** The first argument.

#### `Arg2`
!!!csharp
public T2 Arg2 { get; }
!!!
- **Description:** The second argument.

### Methods

#### `Invoke(T1, T2)`
!!!csharp
public void Invoke(T1 arg1, T2 arg2);
!!!
- **Description:** Marks the request as required and stores both arguments.

#### `Consume(out T1, out T2)`
!!!csharp
public bool Consume(out T1 arg1, out T2 arg2);
!!!
- **Description:** Attempts to consume the request and retrieve both arguments.
- **Output parameters:** `arg1`, `arg2` — the stored arguments.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T1, out T2)`
!!!csharp
public bool TryGet(out T1 arg1, out T2 arg2);
!!!
- **Description:** Attempts to retrieve both arguments without consuming the request.
- **Output parameters:** `arg1`, `arg2` — the stored arguments.
- **Returns:** `true` if the request is currently required.

---

## 🧩 BaseRequest<T1, T2, T3>

!!!csharp
public class BaseRequest<T1, T2, T3> : IRequest<T1, T2, T3>
!!!
- **Description:** Represents a request with three arguments.
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument

### Properties

#### `Required`
!!!csharp
public bool Required { get; }
!!!
- **Description:** Indicates whether the request is currently required.

#### `Arg1`
!!!csharp
public T1 Arg1 { get; }
!!!
- **Description:** The first argument.

#### `Arg2`
!!!csharp
public T2 Arg2 { get; }
!!!
- **Description:** The second argument.

#### `Arg3`
!!!csharp
public T3 Arg3 { get; }
!!!
- **Description:** The third argument.

### Methods

#### `Invoke(T1, T2, T3)`
!!!csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
!!!
- **Description:** Marks the request as required and stores all three arguments.

#### `Consume(out T1, out T2, out T3)`
!!!csharp
public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3);
!!!
- **Description:** Attempts to consume the request and retrieve all arguments.
- **Output parameters:** `arg1`, `arg2`, `arg3` — the stored arguments.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T1, out T2, out T3)`
!!!csharp
public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3);
!!!
- **Description:** Attempts to retrieve all arguments without consuming the request.
- **Output parameters:** `arg1`, `arg2`, `arg3` — the stored arguments.
- **Returns:** `true` if the request is currently required.

---

## 🧩 BaseRequest<T1, T2, T3, T4>

!!!csharp
public class BaseRequest<T1, T2, T3, T4> : IRequest<T1, T2, T3, T4>
!!!
- **Description:** Represents a request with four arguments.
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument
    - `T4` — fourth argument

### Properties

#### `Required`
!!!csharp
public bool Required { get; }
!!!
- **Description:** Indicates whether the request is currently required.

#### `Arg1`
!!!csharp
public T1 Arg1 { get; }
!!!
- **Description:** The first argument.

#### `Arg2`
!!!csharp
public T2 Arg2 { get; }
!!!
- **Description:** The second argument.

#### `Arg3`
!!!csharp
public T3 Arg3 { get; }
!!!
- **Description:** The third argument.

#### `Arg4`
!!!csharp
public T4 Arg4 { get; }
!!!
- **Description:** The fourth argument.

### Methods

#### `Invoke(T1, T2, T3, T4)`
!!!csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
!!!
- **Description:** Marks the request as required and stores all four arguments.

#### `Consume(out T1, out T2, out T3, out T4)`
!!!csharp
public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
!!!
- **Description:** Attempts to consume the request and retrieve all arguments.
- **Output parameters:** `arg1`, `arg2`, `arg3`, `arg4` — the stored arguments.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T1, out T2, out T3, out T4)`
!!!csharp
public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
!!!
- **Description:** Attempts to retrieve all arguments without consuming the request.
- **Output parameters:** `arg1`, `arg2`, `arg3`, `arg4` — the stored arguments.
- **Returns:** `true` if the request is currently required.











========
========

# 🧩 BaseRequest Classes

Base implementations of **IRequest** with 0–4 arguments.  
These classes allow **deferred requests**, which can be invoked and consumed later, preventing duplicate handling.

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
