# 🧩 BaseRequest

The **BaseRequest** classes provide **concrete implementations** of the [IRequest](IRequest.md) interfaces. They are
designed to store request state and optionally one to four arguments. These classes **track whether a request is
required** and allow **deferred consumption**.

> [!IMPORTANT]
> Unlike regular actions, requests are meant for **deferred execution**. You can call `Invoke` to create a request, and
> process it later using `Consume`. Repeated `Invoke` calls before `Consume` do not create duplicates.

---

<details>
  <summary>
    <h2>🧩 BaseRequest</h2>
    <br> Represents a <b>parameterless</b> request action.
  </summary>

<br>

```csharp
public class BaseRequest : IRequest
```

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request must be handled.

---

### 🏹 Methods

#### `Invoke()`

```csharp
public void Invoke();
```

- **Description:** Executes the request.

#### `Consume()`

```csharp
public bool Consume();
```

- **Description:** Attempts to consume the request.
- **Returns:** `true` if successfully consumed; otherwise `false`.

</details>

---

<details>
  <summary>
    <h2>🧩 BaseRequest&lt;T&gt;</h2>
    <br> Represents a request action with <b>one input argument</b>.
  </summary>

<br>

```csharp
public class BaseRequest<T> : IRequest<T>
```

- **Type parameter:** `T` — type of the argument.

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request is currently required.

#### `Arg`

```csharp
public T Arg { get; }
```

- **Description:** The stored argument.

---

### 🏹 Methods

#### `Invoke(T)`

```csharp
public void Invoke(T arg);
```

- **Description:** Marks the request as required and stores the argument.
- **Parameter:** `arg` — the input argument.

#### `Consume(out T)`

```csharp
public bool Consume(out T arg);
```

- **Description:** Attempts to consume the request and retrieve the argument.
- **Output parameter:** `arg` — the argument if successfully consumed.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T)`

```csharp
public bool TryGet(out T arg);
```

- **Description:** Attempts to retrieve the argument without consuming the request.
- **Output parameter:** `arg` — the stored argument.
- **Returns:** `true` if the request is currently required.

</details>

---


<details>
  <summary>
    <h2>🧩 IRequest&lt;T1, T2&gt;</h2>
    <br> Represents a request action with <b>two input arguments</b>.
  </summary>

<br>

```csharp
public class BaseRequest<T1, T2> : IRequest<T1, T2>
```

- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request is currently required.

#### `Arg1`

```csharp
public T1 Arg1 { get; }
```

- **Description:** The first argument.

#### `Arg2`

```csharp
public T2 Arg2 { get; }
```

- **Description:** The second argument.

---

### 🏹 Methods

#### `Invoke(T1, T2)`

```csharp
public void Invoke(T1 arg1, T2 arg2);
```

- **Description:** Marks the request as required and stores both arguments.
- **Parameters:** `arg1`, `arg2` — the input arguments.

#### `Consume(out T1, out T2)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2);
```

- **Description:** Attempts to consume the request and retrieve both arguments.
- **Output parameters:** `arg1`, `arg2` — the stored arguments.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T1, out T2)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2);
```

- **Description:** Attempts to retrieve both arguments without consuming the request.
- **Output parameters:** `arg1`, `arg2` — the stored arguments.
- **Returns:** `true` if the request is currently required.

</details>

---

<details>
  <summary>
    <h2>🧩 BaseRequest&lt;T1, T2, T3&gt;</h2>
    <br> Represents a request action with <b>three input arguments</b>.
  </summary>

<br>

```csharp
public class BaseRequest<T1, T2, T3> : IRequest<T1, T2, T3>
```

- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request is currently required.

#### `Arg1`

```csharp
public T1 Arg1 { get; }
```

- **Description:** The first argument.

#### `Arg2`

```csharp
public T2 Arg2 { get; }
```

- **Description:** The second argument.

#### `Arg3`

```csharp
public T3 Arg3 { get; }
```

- **Description:** The third argument.

---

### 🏹 Methods

#### `Invoke(T1, T2, T3)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3);
```

- **Description:** Marks the request as required and stores all three arguments.
- **Parameters:** `arg1`, `arg2`, `arg3` — the input arguments.

#### `Consume(out T1, out T2, out T3)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3);
```

- **Description:** Attempts to consume the request and retrieve all arguments.
- **Output parameters:** `arg1`, `arg2`, `arg3` — the stored arguments.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T1, out T2, out T3)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3);
```

- **Description:** Attempts to retrieve all arguments without consuming the request.
- **Output parameters:** `arg1`, `arg2`, `arg3` — the stored arguments.
- **Returns:** `true` if the request is currently required.

</details>

---

<details>
  <summary>
    <h2>🧩 BaseRequest&lt;T1, T2, T3, T4&gt;</h2>
    <br> Represents a request action with <b>four input arguments</b>.
  </summary>

<br>

```csharp
public class BaseRequest<T1, T2, T3, T4> : IRequest<T1, T2, T3, T4>
```
- **Type parameters:**
    - `T1` — first argument
    - `T2` — second argument
    - `T3` — third argument
    - `T4` — fourth argument

---

### 🔑 Properties

#### `Required`

```csharp
public bool Required { get; }
```

- **Description:** Indicates whether the request is currently required.

#### `Arg1`

```csharp
public T1 Arg1 { get; }
```

- **Description:** The first argument.

#### `Arg2`

```csharp
public T2 Arg2 { get; }
```

- **Description:** The second argument.

#### `Arg3`

```csharp
public T3 Arg3 { get; }
```

- **Description:** The third argument.

#### `Arg4`

```csharp
public T4 Arg4 { get; }
```

- **Description:** The fourth argument.

---

### 🏹 Methods

#### `Invoke(T1, T2, T3, T4)`

```csharp
public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
```

- **Description:** Marks the request as required and stores all four arguments.
- **Parameters:** `arg1`, `arg2`, `arg3`, `arg4` — the input arguments.

#### `Consume(out T1, out T2, out T3, out T4)`

```csharp
public bool Consume(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
```

- **Description:** Attempts to consume the request and retrieve all arguments.
- **Output parameters:** `arg1`, `arg2`, `arg3`, `arg4` — the stored arguments.
- **Returns:** `true` if the request was required and is now consumed.

#### `TryGet(out T1, out T2, out T3, out T4)`

```csharp
public bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
```

- **Description:** Attempts to retrieve all arguments without consuming the request.
- **Output parameters:** `arg1`, `arg2`, `arg3`, `arg4` — the stored arguments.
- **Returns:** `true` if the request is currently required.

</details>

---

## 🗂 Example of Usage

Below are examples of using `IRequest` with the `Atomic.Entities` framework.

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